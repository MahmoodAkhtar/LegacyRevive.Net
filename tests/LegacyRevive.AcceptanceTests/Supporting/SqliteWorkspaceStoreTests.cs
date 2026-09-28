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
            IntakeSnapshot = invalid.IntakeSnapshot with { ArtifactIds = [ArtifactId.Create()] }
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
