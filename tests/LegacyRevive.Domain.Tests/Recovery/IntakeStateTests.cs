using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Domain.Tests.Recovery;

public sealed class IntakeStateTests
{
    [Fact]
    public void EmptyCompleteSnapshotIsValid()
    {
        var runId = RecoveryRunId.Create();
        var snapshot = new RecoveryIntakeSnapshot(IntakeSnapshotId.Create(), runId, []);
        var state = new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Completed, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "hash"),
            []);

        Assert.True(state.HasSuccessfulIntakeSnapshot);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, state.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Empty(state.Artifacts);
    }

    [Fact]
    public void RootBlockedStateCannotMasqueradeAsSuccessfulEmptySnapshot()
    {
        var runId = RecoveryRunId.Create();
        var diagnostic = RootDiagnostic();
        var invalidSnapshot = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [],
            SuppliedSourceDiscoveryCompleteness.Incomplete);

        Assert.Throws<ArgumentException>(() => new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            invalidSnapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, invalidSnapshot.Id, "hash"),
            [],
            [diagnostic]));

        var blocked = new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Blocked, "tool", "config"),
            null,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, null, "hash"),
            [],
            [diagnostic]);
        Assert.False(blocked.HasSuccessfulIntakeSnapshot);
    }

    [Fact]
    public void PartialRunCanContainIncompleteSnapshotAndNarrowBlockedScope()
    {
        var runId = RecoveryRunId.Create();
        var snapshot = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [],
            SuppliedSourceDiscoveryCompleteness.Incomplete);
        var diagnostic = new IntakeDiscoveryDiagnostic(
            "INTAKE_DISCOVERY_FAILED",
            "Access denied.",
            IntakeDiscoveryScope.DirectorySubtree("blocked"));
        var state = new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "hash"),
            [],
            [diagnostic]);

        Assert.Equal(RecoveryRunOutcome.Partial, state.Run.Outcome);
        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, state.IntakeSnapshot!.DiscoveryCompleteness);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectorySubtree, Assert.Single(state.DiscoveryDiagnostics).Scope.Kind);
    }

    [Fact]
    public void PartialRootDescendantFailureWithPositiveArtifactIsRepresentable()
    {
        var runId = RecoveryRunId.Create();
        var artifact = OriginalArtifact.Pending(
            ArtifactId.Create(),
            runId,
            SuppliedArtifactProvenance.Create("root.bin", "capture"));
        var snapshot = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [artifact.Id],
            SuppliedSourceDiscoveryCompleteness.Incomplete);
        var diagnostic = new IntakeDiscoveryDiagnostic(
            "INTAKE_DISCOVERY_FAILED",
            "Descendant discovery failed.",
            IntakeDiscoveryScope.DirectoryDescendants("."));

        var state = new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "hash"),
            [artifact],
            [diagnostic]);

        Assert.Equal(RecoveryRunOutcome.Partial, state.Run.Outcome);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectoryDescendants, Assert.Single(state.DiscoveryDiagnostics).Scope.Kind);
        Assert.Equal(".", state.DiscoveryDiagnostics[0].Scope.RelativePath);
    }

    [Fact]
    public void RootDescendantFailureWithoutPositiveArtifactRejectsIncompleteEmptySnapshotButAllowsBlockedState()
    {
        var runId = RecoveryRunId.Create();
        var diagnostic = new IntakeDiscoveryDiagnostic(
            "INTAKE_DISCOVERY_FAILED",
            "Descendant discovery failed.",
            IntakeDiscoveryScope.DirectoryDescendants("."));
        var snapshot = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [],
            SuppliedSourceDiscoveryCompleteness.Incomplete);

        Assert.Throws<ArgumentException>(() => new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "hash"),
            [],
            [diagnostic]));

        var blocked = new RecoveryWorkspaceState(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Blocked, "tool", "config"),
            null,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, null, "hash"),
            [],
            [diagnostic]);
        Assert.False(blocked.HasSuccessfulIntakeSnapshot);
    }

    [Fact]
    public void DiscoveryCompletenessAndDiagnosticsMustAgree()
    {
        var runId = RecoveryRunId.Create();
        var complete = new RecoveryIntakeSnapshot(IntakeSnapshotId.Create(), runId, []);
        var incomplete = new RecoveryIntakeSnapshot(
            IntakeSnapshotId.Create(),
            runId,
            [],
            SuppliedSourceDiscoveryCompleteness.Incomplete);

        Assert.Throws<ArgumentException>(() => Create(runId, complete, [RootDiagnostic()]));
        Assert.Throws<ArgumentException>(() => Create(runId, incomplete, []));
    }

    [Fact]
    public void DiscoveryDiagnosticDoesNotRequireArtifactIdentity()
    {
        var diagnostic = new IntakeDiscoveryDiagnostic(
            "INTAKE_DISCOVERY_FAILED",
            "Enumeration failed.",
            IntakeDiscoveryScope.DirectorySubtree("nested"));

        Assert.Equal("nested", diagnostic.Scope.RelativePath);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectorySubtree, diagnostic.Scope.Kind);
    }

    private static RecoveryWorkspaceState Create(
        RecoveryRunId runId,
        RecoveryIntakeSnapshot snapshot,
        IReadOnlyList<IntakeDiscoveryDiagnostic> diagnostics) =>
        new(
            WorkspaceId.Create(),
            new RecoveryRun(runId, RecoveryRunOutcome.Partial, "tool", "config"),
            snapshot,
            new RecoveryCheckpoint(RecoveryCheckpointId.Create(), runId, snapshot.Id, "hash"),
            [],
            diagnostics);

    private static IntakeDiscoveryDiagnostic RootDiagnostic() =>
        new("INTAKE_DISCOVERY_FAILED", "Root failed.", IntakeDiscoveryScope.SuppliedSource());
}
