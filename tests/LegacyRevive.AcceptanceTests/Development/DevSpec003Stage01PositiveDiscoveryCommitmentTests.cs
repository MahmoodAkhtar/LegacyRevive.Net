using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local;
using LegacyRevive.Infrastructure.Local.Artifacts;
using LegacyRevive.Infrastructure.Local.Persistence;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.AcceptanceTests.Development;

public sealed class DevSpec003Stage01PositiveDiscoveryCommitmentTests
{
    [Fact]
    public async Task S1_RootPositiveFileSurvivesLaterRootDescendantDiscoveryFailure()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "root.dll"), "root", TestContext.Current.CancellationToken);

        var session = await CreateService(
                Path.Combine(test.Root, "workspace"),
                new DescendantFailingEnumerator(source))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, session.State.IntakeSnapshot!.DiscoveryCompleteness);
        var artifact = Assert.Single(session.State.Artifacts);
        Assert.Equal("root.dll", artifact.Provenance.RelativePath);
        Assert.Equal(ArtifactPreservationStatus.Preserved, artifact.PreservationStatus);
        Assert.Equal([artifact.Id], session.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal("root"u8.ToArray(), await session.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        AssertRootDescendantFailure(session.State);
    }

    [Fact]
    public async Task S2_NestedPositiveFileAndSafeSiblingSurviveLaterNestedDescendantDiscoveryFailure()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var nested = Directory.CreateDirectory(Path.Combine(source, "nested")).FullName;
        await File.WriteAllTextAsync(Path.Combine(source, "safe.bin"), "safe", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(nested, "nested.bin"), "nested", TestContext.Current.CancellationToken);

        var session = await CreateService(
                Path.Combine(test.Root, "workspace"),
                new DescendantFailingEnumerator(nested))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, session.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Equal(["nested/nested.bin", "safe.bin"], session.State.Artifacts.Select(artifact => artifact.Provenance.RelativePath));
        Assert.All(session.State.Artifacts, artifact => Assert.Equal(ArtifactPreservationStatus.Preserved, artifact.PreservationStatus));
        var diagnostic = Assert.Single(session.State.DiscoveryDiagnostics);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectoryDescendants, diagnostic.Scope.Kind);
        Assert.Equal("nested", diagnostic.Scope.RelativePath);
    }

    [Fact]
    public async Task S3_ZeroRootDirectFilesThenDescendantFailureIsBlockedWithoutSnapshot()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");

        var session = await CreateService(
                Path.Combine(test.Root, "workspace"),
                new DescendantFailingEnumerator(source))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Blocked, session.State.Run.Outcome);
        Assert.Null(session.State.IntakeSnapshot);
        Assert.Null(session.State.Checkpoint.IntakeSnapshotId);
        Assert.Empty(session.State.Artifacts);
        AssertRootDescendantFailure(session.State);
    }

    [Fact]
    public async Task S4_NestedZeroFileDescendantFailureRetainsOtherPositiveArtifactOnly()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var nested = Directory.CreateDirectory(Path.Combine(source, "nested-empty")).FullName;
        await File.WriteAllTextAsync(Path.Combine(source, "safe.bin"), "safe", TestContext.Current.CancellationToken);

        var session = await CreateService(
                Path.Combine(test.Root, "workspace"),
                new DescendantFailingEnumerator(nested))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, session.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Equal("safe.bin", Assert.Single(session.State.Artifacts).Provenance.RelativePath);
        var diagnostic = Assert.Single(session.State.DiscoveryDiagnostics);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectoryDescendants, diagnostic.Scope.Kind);
        Assert.Equal("nested-empty", diagnostic.Scope.RelativePath);
    }

    [Fact]
    public async Task S5_ReopenPartialRootIntakeWithoutOriginalSourceTreePreservesHistoricalState()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "root.dll"), "root", TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var created = await CreateService(workspace, new DescendantFailingEnumerator(source))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        Directory.Delete(source, recursive: true);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace)
            .OpenAsync(TestContext.Current.CancellationToken);

        Assert.Equal(created.State.Run, reopened.State.Run);
        Assert.Equal(created.State.IntakeSnapshot!.Id, reopened.State.IntakeSnapshot!.Id);
        Assert.Equal(created.State.IntakeSnapshot.RecoveryRunId, reopened.State.IntakeSnapshot.RecoveryRunId);
        Assert.Equal(created.State.IntakeSnapshot.DiscoveryCompleteness, reopened.State.IntakeSnapshot.DiscoveryCompleteness);
        Assert.Equal(created.State.IntakeSnapshot.ArtifactIds, reopened.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(created.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(created.State.DiscoveryDiagnostics, reopened.State.DiscoveryDiagnostics);
        Assert.Equal(created.State.Artifacts, reopened.State.Artifacts);
        var artifact = Assert.Single(reopened.State.Artifacts);
        Assert.Equal("root"u8.ToArray(), await reopened.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        AssertRootDescendantFailure(reopened.State);
    }

    [Fact]
    public async Task S6_EquivalentPositiveResultsWithDifferentFileOrderingAreSubstantivelyDeterministic()
    {
        using var test = new TestWorkspace();
        var firstSource = test.CreateDirectory("first-source");
        var secondSource = test.CreateDirectory("second-source");
        await WriteEquivalentFiles(firstSource);
        await WriteEquivalentFiles(secondSource);
        var diagnostic = RootDescendantDiagnostic("Same descendant failure.");
        var firstDiscovery = SuppliedFileDiscoveryResult.Incomplete(
            [new SuppliedFile("z.bin", Path.Combine(firstSource, "z.bin")), new SuppliedFile("a.bin", Path.Combine(firstSource, "a.bin"))],
            [diagnostic]);
        var secondDiscovery = SuppliedFileDiscoveryResult.Incomplete(
            [new SuppliedFile("a.bin", Path.Combine(secondSource, "a.bin")), new SuppliedFile("z.bin", Path.Combine(secondSource, "z.bin"))],
            [diagnostic]);

        var first = await CreateService(Path.Combine(test.Root, "first-workspace"), new FixedEnumerator(firstDiscovery))
            .CreateAndIntakeAsync(new IntakeRequest(firstSource, "controlled", "tool", "config"), TestContext.Current.CancellationToken);
        var second = await CreateService(Path.Combine(test.Root, "second-workspace"), new FixedEnumerator(secondDiscovery))
            .CreateAndIntakeAsync(new IntakeRequest(secondSource, "controlled", "tool", "config"), TestContext.Current.CancellationToken);

        Assert.Equal(["a.bin", "z.bin"], first.State.Artifacts.Select(artifact => artifact.Provenance.RelativePath));
        Assert.Equal(first.State.Artifacts.Select(SubstantiveArtifact), second.State.Artifacts.Select(SubstantiveArtifact));
        Assert.Equal(first.State.Run.Outcome, second.State.Run.Outcome);
        Assert.Equal(first.State.IntakeSnapshot!.DiscoveryCompleteness, second.State.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Equal(first.State.DiscoveryDiagnostics, second.State.DiscoveryDiagnostics);
        Assert.Equal(first.State.Checkpoint.SubstanceHash, second.State.Checkpoint.SubstanceHash);
    }

    [Fact]
    public async Task S7_TotalRootFailureRemainsBlockedWithoutSnapshotOrArtifacts()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var discovery = SuppliedFileDiscoveryResult.Incomplete(
            [],
            [new IntakeDiscoveryDiagnostic("INTAKE_DISCOVERY_FAILED", "Root failed.", IntakeDiscoveryScope.SuppliedSource())]);

        var session = await CreateService(Path.Combine(test.Root, "workspace"), new FixedEnumerator(discovery))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Blocked, session.State.Run.Outcome);
        Assert.Null(session.State.IntakeSnapshot);
        Assert.Empty(session.State.Artifacts);
        Assert.Equal(IntakeDiscoveryScopeKind.SuppliedSource, Assert.Single(session.State.DiscoveryDiagnostics).Scope.Kind);
    }

    [Fact]
    public async Task S8_PositiveFilePreservationFailureRemainsArtifactScopedBesideDescendantFailure()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "vanishes.bin"), "gone", TestContext.Current.CancellationToken);
        var enumerator = new DeleteAfterDiscoveryEnumerator(new DescendantFailingEnumerator(source), "vanishes.bin");

        var session = await CreateService(Path.Combine(test.Root, "workspace"), enumerator)
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        var artifact = Assert.Single(session.State.Artifacts);
        Assert.Equal(ArtifactPreservationStatus.Failed, artifact.PreservationStatus);
        Assert.Equal("INTAKE_PRESERVATION_FAILED", artifact.Diagnostic?.Code);
        Assert.Contains(artifact.Id, session.State.IntakeSnapshot!.ArtifactIds);
        AssertRootDescendantFailure(session.State);
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

    private static void AssertRootDescendantFailure(RecoveryWorkspaceState state)
    {
        var diagnostic = Assert.Single(state.DiscoveryDiagnostics);
        Assert.Equal("INTAKE_DISCOVERY_FAILED", diagnostic.Code);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectoryDescendants, diagnostic.Scope.Kind);
        Assert.Equal(".", diagnostic.Scope.RelativePath);
        Assert.DoesNotContain(state.DiscoveryDiagnostics, item => item.Scope.Kind == IntakeDiscoveryScopeKind.SuppliedSource);
    }

    private static IntakeDiscoveryDiagnostic RootDescendantDiagnostic(string message) =>
        new("INTAKE_DISCOVERY_FAILED", message, IntakeDiscoveryScope.DirectoryDescendants("."));

    private static async Task WriteEquivalentFiles(string source)
    {
        await File.WriteAllTextAsync(Path.Combine(source, "a.bin"), "a", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "z.bin"), "z", TestContext.Current.CancellationToken);
    }

    private static (string Path, string? Hash, ArtifactPreservationStatus Status, string? DiagnosticCode) SubstantiveArtifact(OriginalArtifact artifact) =>
        (artifact.Provenance.RelativePath, artifact.ContentHash?.Value, artifact.PreservationStatus, artifact.Diagnostic?.Code);

    private sealed class DescendantFailingEnumerator(string failingDirectory) : LocalSuppliedFileEnumerator
    {
        private readonly string _failingDirectory = Path.GetFullPath(failingDirectory);

        protected override IEnumerable<string> EnumerateDirectories(string directoryPath)
        {
            if (string.Equals(Path.GetFullPath(directoryPath), _failingDirectory, StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("Injected descendant discovery failure.");
            }

            return base.EnumerateDirectories(directoryPath);
        }
    }

    private sealed class FixedEnumerator(SuppliedFileDiscoveryResult result) : ISuppliedFileEnumerator
    {
        public Task<SuppliedFileDiscoveryResult> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(result);
        }
    }

    private sealed class DeleteAfterDiscoveryEnumerator(ISuppliedFileEnumerator inner, string relativePath) : ISuppliedFileEnumerator
    {
        public async Task<SuppliedFileDiscoveryResult> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default)
        {
            var result = await inner.EnumerateAsync(suppliedDirectory, cancellationToken);
            File.Delete(result.Files.Single(file => file.RelativePath == relativePath).SourcePath);
            return result;
        }
    }
}
