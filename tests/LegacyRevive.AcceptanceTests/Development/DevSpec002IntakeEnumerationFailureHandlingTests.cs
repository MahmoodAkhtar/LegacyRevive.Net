using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local;
using LegacyRevive.Infrastructure.Local.Artifacts;
using LegacyRevive.Infrastructure.Local.Persistence;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.AcceptanceTests.Development;

public sealed class DevSpec002IntakeEnumerationFailureHandlingTests
{
    [Fact]
    public async Task S1_CompletelyEnumerableEmptySuppliedDirectoryIsKnownEmptyAndReplayable()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var workspace = Path.Combine(test.Root, "workspace");

        var created = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        var reopened = await new LocalRecoveryWorkspaceApplication(workspace)
            .OpenAsync(TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Completed, created.State.Run.Outcome);
        Assert.True(created.State.HasSuccessfulIntakeSnapshot);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, created.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Empty(created.State.IntakeSnapshot.ArtifactIds);
        Assert.Empty(created.State.Artifacts);
        Assert.Empty(created.State.DiscoveryDiagnostics);
        Assert.Equal(created.State.Run, reopened.State.Run);
        Assert.Equal(created.State.IntakeSnapshot!.Id, reopened.State.IntakeSnapshot!.Id);
        Assert.Equal(created.State.IntakeSnapshot.RecoveryRunId, reopened.State.IntakeSnapshot.RecoveryRunId);
        Assert.Equal(created.State.IntakeSnapshot.ArtifactIds, reopened.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(created.State.Checkpoint, reopened.State.Checkpoint);
    }

    [Fact]
    public async Task S2_SuppliedRootFailureIsBlockedWithoutSnapshotOrHypotheticalArtifacts()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var workspace = Path.Combine(test.Root, "workspace");
        var session = await CreateService(
                workspace,
                new FixedDiscoveryEnumerator(RootFailure("Injected root enumeration failure.")))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Blocked, session.State.Run.Outcome);
        Assert.False(session.State.HasSuccessfulIntakeSnapshot);
        Assert.Null(session.State.IntakeSnapshot);
        Assert.Null(session.State.Checkpoint.IntakeSnapshotId);
        Assert.Empty(session.State.Artifacts);
        var diagnostic = Assert.Single(session.State.DiscoveryDiagnostics);
        Assert.Equal("INTAKE_DISCOVERY_FAILED", diagnostic.Code);
        Assert.Equal(IntakeDiscoveryScopeKind.SuppliedSource, diagnostic.Scope.Kind);
        Assert.Equal(".", diagnostic.Scope.RelativePath);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);
        Assert.Equal(session.State.Run, reopened.State.Run);
        Assert.Null(reopened.State.IntakeSnapshot);
        Assert.Equal(session.State.DiscoveryDiagnostics, reopened.State.DiscoveryDiagnostics);
        Assert.Empty(reopened.State.Artifacts);
    }

    [Fact]
    public async Task S3_SubtreeFailurePreservesSiblingAndCreatesIncompletePartialSnapshot()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var siblingPath = Path.Combine(source, "safe.bin");
        await File.WriteAllTextAsync(siblingPath, "safe", TestContext.Current.CancellationToken);
        var discovery = PartialDiscovery(
            [new SuppliedFile("safe.bin", siblingPath)],
            "blocked/subtree",
            "Injected subtree enumeration failure.");

        var session = await CreateService(Path.Combine(test.Root, "workspace"), new FixedDiscoveryEnumerator(discovery))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, session.State.IntakeSnapshot!.DiscoveryCompleteness);
        var artifact = Assert.Single(session.State.Artifacts);
        Assert.Equal("safe.bin", artifact.Provenance.RelativePath);
        Assert.Equal(ArtifactPreservationStatus.Preserved, artifact.PreservationStatus);
        Assert.Equal([artifact.Id], session.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal("safe"u8.ToArray(), await session.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        var diagnostic = Assert.Single(session.State.DiscoveryDiagnostics);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectorySubtree, diagnostic.Scope.Kind);
        Assert.Equal("blocked/subtree", diagnostic.Scope.RelativePath);
        Assert.DoesNotContain(session.State.Artifacts, item => item.Provenance.RelativePath.StartsWith("blocked/", StringComparison.Ordinal));
    }

    [Fact]
    public async Task S4_DiscoveredFileDisappearingBeforePreservationRemainsArtifactScoped()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "good.bin"), "good", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "vanishes.bin"), "gone", TestContext.Current.CancellationToken);
        var enumerator = new DeleteDiscoveredFileEnumerator(new LocalSuppliedFileEnumerator(), "vanishes.bin");

        var session = await CreateService(Path.Combine(test.Root, "workspace"), enumerator)
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, session.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Empty(session.State.DiscoveryDiagnostics);
        var failed = session.State.Artifacts.Single(artifact => artifact.Provenance.RelativePath == "vanishes.bin");
        Assert.Equal(ArtifactPreservationStatus.Failed, failed.PreservationStatus);
        Assert.Equal("INTAKE_PRESERVATION_FAILED", failed.Diagnostic?.Code);
        Assert.Contains(failed.Id, session.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(ArtifactPreservationStatus.Preserved,
            session.State.Artifacts.Single(artifact => artifact.Provenance.RelativePath == "good.bin").PreservationStatus);
    }

    [Fact]
    public async Task S5_ReopenPartialSnapshotDoesNotReenumerateDeletedExternalTree()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var siblingPath = Path.Combine(source, "safe.bin");
        await File.WriteAllTextAsync(siblingPath, "safe", TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var created = await CreateService(
                workspace,
                new FixedDiscoveryEnumerator(PartialDiscovery(
                    [new SuppliedFile("safe.bin", siblingPath)],
                    "blocked",
                    "Injected subtree enumeration failure.")))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        Directory.Delete(source, recursive: true);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);

        Assert.Equal(created.State.Run, reopened.State.Run);
        Assert.Equal(RecoveryRunOutcome.Partial, reopened.State.Run.Outcome);
        Assert.Equal(created.State.IntakeSnapshot!.Id, reopened.State.IntakeSnapshot!.Id);
        Assert.Equal(created.State.IntakeSnapshot.RecoveryRunId, reopened.State.IntakeSnapshot.RecoveryRunId);
        Assert.Equal(created.State.IntakeSnapshot.ArtifactIds, reopened.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, reopened.State.IntakeSnapshot.DiscoveryCompleteness);
        Assert.Equal(created.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(created.State.DiscoveryDiagnostics, reopened.State.DiscoveryDiagnostics);
        Assert.Equal(created.State.Artifacts, reopened.State.Artifacts);
        var artifact = Assert.Single(reopened.State.Artifacts);
        Assert.Equal("safe"u8.ToArray(), await reopened.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task S6_EquivalentIncompleteDiscoveryOrdersProduceEquivalentSubstantiveState()
    {
        using var test = new TestWorkspace();
        var firstSource = test.CreateDirectory("source-one");
        var secondSource = test.CreateDirectory("source-two");
        await WriteEquivalentFiles(firstSource);
        await WriteEquivalentFiles(secondSource);
        var firstFiles = new[]
        {
            new SuppliedFile("z.bin", Path.Combine(firstSource, "z.bin")),
            new SuppliedFile("a.bin", Path.Combine(firstSource, "a.bin"))
        };
        var secondFiles = new[]
        {
            new SuppliedFile("a.bin", Path.Combine(secondSource, "a.bin")),
            new SuppliedFile("z.bin", Path.Combine(secondSource, "z.bin"))
        };

        var first = await CreateService(
                Path.Combine(test.Root, "workspace-one"),
                new FixedDiscoveryEnumerator(PartialDiscovery(firstFiles, "blocked", "Same failure.")))
            .CreateAndIntakeAsync(new IntakeRequest(firstSource, "controlled", "tool", "config"), TestContext.Current.CancellationToken);
        var second = await CreateService(
                Path.Combine(test.Root, "workspace-two"),
                new FixedDiscoveryEnumerator(PartialDiscovery(secondFiles, "blocked", "Same failure.")))
            .CreateAndIntakeAsync(new IntakeRequest(secondSource, "controlled", "tool", "config"), TestContext.Current.CancellationToken);

        Assert.Equal(["a.bin", "z.bin"], first.State.Artifacts.Select(item => item.Provenance.RelativePath));
        Assert.Equal(
            first.State.Artifacts.Select(SubstantiveArtifact),
            second.State.Artifacts.Select(SubstantiveArtifact));
        Assert.Equal(first.State.Run.Outcome, second.State.Run.Outcome);
        Assert.Equal(first.State.IntakeSnapshot!.DiscoveryCompleteness, second.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Equal(first.State.DiscoveryDiagnostics, second.State.DiscoveryDiagnostics);
        Assert.Equal(first.State.Checkpoint.SubstanceHash, second.State.Checkpoint.SubstanceHash);
        Assert.Empty(first.State.Artifacts.Select(item => item.Id).Intersect(second.State.Artifacts.Select(item => item.Id)));
    }

    private static ArtifactIntakeService CreateService(string workspace, ISuppliedFileEnumerator enumerator)
    {
        var boundary = new LocalWorkspaceBoundary(workspace);
        return new ArtifactIntakeService(
            boundary,
            enumerator,
            new LocalOriginalArtifactStore(boundary),
            new SqliteIntakeStateRepository(boundary));
    }

    private static SuppliedFileDiscoveryResult RootFailure(string message) =>
        SuppliedFileDiscoveryResult.Incomplete(
            [],
            [new IntakeDiscoveryDiagnostic("INTAKE_DISCOVERY_FAILED", message, IntakeDiscoveryScope.SuppliedSource())]);

    private static SuppliedFileDiscoveryResult PartialDiscovery(
        IReadOnlyList<SuppliedFile> files,
        string failedScope,
        string message) =>
        SuppliedFileDiscoveryResult.Incomplete(
            files,
            [new IntakeDiscoveryDiagnostic(
                "INTAKE_DISCOVERY_FAILED",
                message,
                IntakeDiscoveryScope.DirectorySubtree(failedScope))]);

    private static async Task WriteEquivalentFiles(string root)
    {
        await File.WriteAllTextAsync(Path.Combine(root, "a.bin"), "a", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(root, "z.bin"), "z", TestContext.Current.CancellationToken);
    }

    private static (string Path, string? Hash, ArtifactPreservationStatus Status, string? DiagnosticCode) SubstantiveArtifact(
        OriginalArtifact artifact) =>
        (artifact.Provenance.RelativePath, artifact.ContentHash?.Value, artifact.PreservationStatus, artifact.Diagnostic?.Code);

    private sealed class FixedDiscoveryEnumerator(SuppliedFileDiscoveryResult result) : ISuppliedFileEnumerator
    {
        public Task<SuppliedFileDiscoveryResult> EnumerateAsync(
            string suppliedDirectory,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(result);
        }
    }

    private sealed class DeleteDiscoveredFileEnumerator(ISuppliedFileEnumerator inner, string relativePath) : ISuppliedFileEnumerator
    {
        public async Task<SuppliedFileDiscoveryResult> EnumerateAsync(
            string suppliedDirectory,
            CancellationToken cancellationToken = default)
        {
            var result = await inner.EnumerateAsync(suppliedDirectory, cancellationToken);
            File.Delete(result.Files.Single(file => file.RelativePath == relativePath).SourcePath);
            return result;
        }
    }
}
