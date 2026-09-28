using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;

namespace LegacyRevive.Domain.Recovery;

public enum RecoveryRunOutcome
{
    Completed = 0,
    Partial = 1
}

public sealed record RecoveryRun(
    RecoveryRunId Id,
    RecoveryRunOutcome Outcome,
    string ToolIdentity,
    string ConfigurationIdentity);

public sealed record RecoveryIntakeSnapshot(
    IntakeSnapshotId Id,
    RecoveryRunId RecoveryRunId,
    IReadOnlyList<ArtifactId> ArtifactIds);

public sealed record RecoveryCheckpoint(
    RecoveryCheckpointId Id,
    RecoveryRunId RecoveryRunId,
    IntakeSnapshotId IntakeSnapshotId,
    string SubstanceHash);

public sealed record RecoveryWorkspaceState(
    WorkspaceId WorkspaceId,
    RecoveryRun Run,
    RecoveryIntakeSnapshot IntakeSnapshot,
    RecoveryCheckpoint Checkpoint,
    IReadOnlyList<OriginalArtifact> Artifacts)
{
    public OriginalArtifact GetArtifact(ArtifactId id) =>
        Artifacts.SingleOrDefault(artifact => artifact.Id == id)
        ?? throw new KeyNotFoundException($"Artifact '{id.Value}' is not present in this intake snapshot.");
}
