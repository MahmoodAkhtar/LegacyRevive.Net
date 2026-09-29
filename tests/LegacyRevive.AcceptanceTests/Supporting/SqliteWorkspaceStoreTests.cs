using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local;
using LegacyRevive.Infrastructure.Local.Persistence;
using LegacyRevive.Infrastructure.Local.Workspace;
using Microsoft.Data.Sqlite;

namespace LegacyRevive.AcceptanceTests.Supporting;

public sealed class SqliteWorkspaceStoreTests
{
    [Fact]
    public async Task SqliteRoundTripRetainsWorkspaceInventoryProvenancePreservationStateRunSnapshotCheckpointAndStableArtifactStateAcrossReopen()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("source");
        await File.WriteAllTextAsync(Path.Combine(source, "input.bin"), "input", TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var created = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source, "capture-context", "tool-v1", "config-v1"), TestContext.Current.CancellationToken);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);

        Assert.Equal(created.State.WorkspaceId, reopened.State.WorkspaceId);
        Assert.Equal(created.State.Run, reopened.State.Run);
        Assert.Equal(created.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(created.State.Artifacts, reopened.State.Artifacts);
        Assert.Equal("capture-context", reopened.State.Artifacts.Single().Provenance.CaptureContext);
    }

    [Fact]
    public async Task LaterCheckpointDoesNotRewriteEarlierCheckpoint()
    {
        using var test = new TestWorkspace();
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var repository = new SqliteIntakeStateRepository(boundary);
        var workspaceId = WorkspaceId.Create();
        await repository.InitializeAsync(workspaceId, TestContext.Current.CancellationToken);
        var first = CreateState(workspaceId, "first.bin", "first-hash");
        var second = CreateState(workspaceId, "second.bin", "second-hash");
        await repository.SaveAsync(first, TestContext.Current.CancellationToken);
        await repository.SaveAsync(second, TestContext.Current.CancellationToken);

        var current = await repository.LoadCurrentAsync(TestContext.Current.CancellationToken);
        Assert.Equal(second.Checkpoint, current.Checkpoint);

        await using var connection = OpenDatabase(boundary);
        await connection.OpenAsync(TestContext.Current.CancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT checkpoint_id, substance_hash FROM recovery_checkpoint ORDER BY rowid;";
        await using var reader = await command.ExecuteReaderAsync(TestContext.Current.CancellationToken);
        Assert.True(await reader.ReadAsync(TestContext.Current.CancellationToken));
        Assert.Equal(first.Checkpoint.Id.Value.ToString("D"), reader.GetString(0));
        Assert.Equal("first-hash", reader.GetString(1));
        Assert.True(await reader.ReadAsync(TestContext.Current.CancellationToken));
        Assert.Equal(second.Checkpoint.Id.Value.ToString("D"), reader.GetString(0));
        Assert.Equal("second-hash", reader.GetString(1));
        Assert.False(await reader.ReadAsync(TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task FailedSaveRollsBackAllNewCanonicalStateAndLeavesCurrentCheckpointUnchanged()
    {
        using var test = new TestWorkspace();
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var repository = new SqliteIntakeStateRepository(boundary);
        var workspaceId = WorkspaceId.Create();
        await repository.InitializeAsync(workspaceId, TestContext.Current.CancellationToken);
        var valid = CreateState(workspaceId, "valid.bin", "valid-hash");
        await repository.SaveAsync(valid, TestContext.Current.CancellationToken);
        var invalid = CreateState(workspaceId, "invalid.bin", "invalid-hash");
        invalid = invalid with
        {
            IntakeSnapshot = invalid.IntakeSnapshot! with { ArtifactIds = [ArtifactId.Create()] }
        };

        await Assert.ThrowsAsync<SqliteException>(() => repository.SaveAsync(invalid, TestContext.Current.CancellationToken));

        var current = await repository.LoadCurrentAsync(TestContext.Current.CancellationToken);
        Assert.Equal(valid.Checkpoint, current.Checkpoint);
        await using var connection = OpenDatabase(boundary);
        await connection.OpenAsync(TestContext.Current.CancellationToken);
        await using var count = connection.CreateCommand();
        count.CommandText = "SELECT COUNT(*) FROM recovery_run;";
        Assert.Equal(1L, (long)(await count.ExecuteScalarAsync(TestContext.Current.CancellationToken))!);
    }

    [Fact]
    public async Task IncompleteDiscoveryStateAndPartialOutcomeRoundTripWithAssociations()
    {
        using var test = new TestWorkspace();
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var repository = new SqliteIntakeStateRepository(boundary);
        var workspaceId = WorkspaceId.Create();
        await repository.InitializeAsync(workspaceId, TestContext.Current.CancellationToken);
        var runId = RecoveryRunId.Create();
        var artifact = OriginalArtifact.Pending(
                ArtifactId.Create(),
                runId,
                SuppliedArtifactProvenance.Create("safe.bin", "capture"))
            .MarkPreserved(Sha256Hash.FromHex(new string('b', 64)), ".legacyrevive/artifacts/safe.bin");
        var snapshot = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [artifact.Id],
            SuppliedSourceDiscoveryCompleteness.Incomplete);
        var diagnostic = new IntakeDiscoveryDiagnostic(
            "INTAKE_DISCOVERY_FAILED",
            "Injected failure.",
            IntakeDiscoveryScope.DirectorySubtree("blocked"));
        var state = new RecoveryWorkspaceState(
            workspaceId,
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "partial-hash"),
            [artifact],
            [diagnostic]);

        await repository.SaveAsync(state, TestContext.Current.CancellationToken);
        var reopened = await repository.LoadCurrentAsync(TestContext.Current.CancellationToken);

        Assert.Equal(state.Run, reopened.Run);
        Assert.Equal(state.IntakeSnapshot!.Id, reopened.IntakeSnapshot!.Id);
        Assert.Equal(state.IntakeSnapshot.RecoveryRunId, reopened.IntakeSnapshot.RecoveryRunId);
        Assert.Equal(state.IntakeSnapshot.ArtifactIds, reopened.IntakeSnapshot.ArtifactIds);
        Assert.Equal(state.IntakeSnapshot.DiscoveryCompleteness, reopened.IntakeSnapshot.DiscoveryCompleteness);
        Assert.Equal(state.Checkpoint, reopened.Checkpoint);
        Assert.Equal(state.Artifacts, reopened.Artifacts);
        Assert.Equal(state.DiscoveryDiagnostics, reopened.DiscoveryDiagnostics);
    }

    [Fact]
    public async Task RootBlockedRunRoundTripsWithoutSnapshotAndDiffersFromCompleteEmpty()
    {
        using var test = new TestWorkspace();
        var blockedBoundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "blocked-workspace"));
        await blockedBoundary.CreateAsync(TestContext.Current.CancellationToken);
        var blockedRepository = new SqliteIntakeStateRepository(blockedBoundary);
        var workspaceId = WorkspaceId.Create();
        await blockedRepository.InitializeAsync(workspaceId, TestContext.Current.CancellationToken);
        var runId = RecoveryRunId.Create();
        var blocked = new RecoveryWorkspaceState(
            workspaceId,
            new RecoveryRun(runId, RecoveryRunOutcome.Blocked, "tool", "config"),
            null,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, null, "blocked-hash"),
            [],
            [new IntakeDiscoveryDiagnostic(
                "INTAKE_DISCOVERY_FAILED",
                "Root failed.",
                IntakeDiscoveryScope.SuppliedSource())]);

        await blockedRepository.SaveAsync(blocked, TestContext.Current.CancellationToken);
        var reopened = await blockedRepository.LoadCurrentAsync(TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Blocked, reopened.Run.Outcome);
        Assert.Null(reopened.IntakeSnapshot);
        Assert.Null(reopened.Checkpoint.IntakeSnapshotId);
        Assert.Empty(reopened.Artifacts);
        Assert.Equal(blocked.DiscoveryDiagnostics, reopened.DiscoveryDiagnostics);

        var emptyBoundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "empty-workspace"));
        await emptyBoundary.CreateAsync(TestContext.Current.CancellationToken);
        var emptyRepository = new SqliteIntakeStateRepository(emptyBoundary);
        var emptyWorkspaceId = WorkspaceId.Create();
        await emptyRepository.InitializeAsync(emptyWorkspaceId, TestContext.Current.CancellationToken);
        var emptyRunId = RecoveryRunId.Create();
        var emptySnapshot = new RecoveryIntakeSnapshot(IntakeSnapshotId.Create(), emptyRunId, []);
        await emptyRepository.SaveAsync(new RecoveryWorkspaceState(
            emptyWorkspaceId,
            new RecoveryRun(emptyRunId, RecoveryRunOutcome.Completed, "tool", "config"),
            emptySnapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), emptyRunId, emptySnapshot.Id, "empty-hash"),
            []), TestContext.Current.CancellationToken);
        var empty = await emptyRepository.LoadCurrentAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(empty.IntakeSnapshot);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, empty.IntakeSnapshot.DiscoveryCompleteness);
        Assert.Equal(RecoveryRunOutcome.Completed, empty.Run.Outcome);
    }

    [Fact]
    public async Task DevSpec001SchemaMigratesWithoutChangingHistoricalCheckpointState()
    {
        using var test = new TestWorkspace();
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var workspaceId = WorkspaceId.Create();
        var runId = RecoveryRunId.Create();
        var snapshotId = IntakeSnapshotId.Create();
        var checkpointId = RecoveryCheckpointId.Create();
        await using (var connection = OpenDatabase(boundary))
        {
            await connection.OpenAsync(TestContext.Current.CancellationToken);
            await using var create = connection.CreateCommand();
            create.CommandText = """
                CREATE TABLE workspace (workspace_id TEXT PRIMARY KEY, current_checkpoint_id TEXT NULL);
                CREATE TABLE recovery_run (
                    run_id TEXT PRIMARY KEY, outcome INTEGER NOT NULL,
                    tool_identity TEXT NOT NULL, configuration_identity TEXT NOT NULL);
                CREATE TABLE intake_snapshot (
                    snapshot_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id));
                CREATE TABLE recovery_checkpoint (
                    checkpoint_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    snapshot_id TEXT NOT NULL REFERENCES intake_snapshot(snapshot_id),
                    substance_hash TEXT NOT NULL);
                INSERT INTO workspace VALUES ($workspace, $checkpoint);
                INSERT INTO recovery_run VALUES ($run, 0, 'tool', 'config');
                INSERT INTO intake_snapshot VALUES ($snapshot, $run);
                INSERT INTO recovery_checkpoint VALUES ($checkpoint, $run, $snapshot, 'legacy-hash');
                """;
            create.Parameters.AddWithValue("$workspace", workspaceId.Value.ToString("D"));
            create.Parameters.AddWithValue("$run", runId.Value.ToString("D"));
            create.Parameters.AddWithValue("$snapshot", snapshotId.Value.ToString("D"));
            create.Parameters.AddWithValue("$checkpoint", checkpointId.Value.ToString("D"));
            await create.ExecuteNonQueryAsync(TestContext.Current.CancellationToken);
        }

        var migrated = await new SqliteIntakeStateRepository(boundary)
            .LoadCurrentAsync(TestContext.Current.CancellationToken);

        Assert.Equal(workspaceId, migrated.WorkspaceId);
        Assert.Equal(runId, migrated.Run.Id);
        Assert.Equal(snapshotId, migrated.IntakeSnapshot!.Id);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, migrated.IntakeSnapshot.DiscoveryCompleteness);
        Assert.Equal(checkpointId, migrated.Checkpoint.Id);
        Assert.Equal("legacy-hash", migrated.Checkpoint.SubstanceHash);
        Assert.Empty(migrated.Artifacts);
        Assert.Empty(migrated.DiscoveryDiagnostics);
    }

    private static RecoveryWorkspaceState CreateState(WorkspaceId workspaceId, string relativePath, string substanceHash)
    {
        var runId = RecoveryRunId.Create();
        var artifact = OriginalArtifact.Pending(
                ArtifactId.Create(),
                runId,
                SuppliedArtifactProvenance.Create(relativePath, "test"))
            .MarkPreserved(Sha256Hash.FromHex(new string('a', 64)), $".legacyrevive/artifacts/{Guid.NewGuid():N}.bin");
        var run = new RecoveryRun(runId, RecoveryRunOutcome.Completed, "tool", "config");
        var snapshot = new RecoveryIntakeSnapshot(IntakeSnapshotId.Create(), runId, [artifact.Id]);
        var checkpoint = new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, substanceHash);
        return new RecoveryWorkspaceState(workspaceId, run, snapshot, checkpoint, [artifact]);
    }

    private static SqliteConnection OpenDatabase(LocalWorkspaceBoundary boundary) =>
        new(new SqliteConnectionStringBuilder
        {
            DataSource = Path.Combine(boundary.InternalStateRoot, "workspace.db"),
            Pooling = false
        }.ToString());
}
