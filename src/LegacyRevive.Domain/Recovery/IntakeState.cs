using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;

namespace LegacyRevive.Domain.Recovery;

public enum RecoveryRunOutcome
{
    Completed = 0,
    Partial = 1,
    Blocked = 2
}

public enum SuppliedSourceDiscoveryCompleteness
{
    Complete = 0,
    Incomplete = 1
}

public enum IntakeDiscoveryScopeKind
{
    SuppliedSource = 0,
    DirectorySubtree = 1,
    DirectoryDescendants = 2
}

public sealed record IntakeDiscoveryScope
{
    private IntakeDiscoveryScope(IntakeDiscoveryScopeKind kind, string relativePath)
    {
        Kind = kind;
        RelativePath = relativePath;
    }

    public IntakeDiscoveryScopeKind Kind { get; }
    public string RelativePath { get; }

    public static IntakeDiscoveryScope SuppliedSource() => new(IntakeDiscoveryScopeKind.SuppliedSource, ".");

    public static IntakeDiscoveryScope DirectorySubtree(string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);
        if (Path.IsPathRooted(relativePath))
        {
            throw new ArgumentException("Discovery scope must be relative to the supplied source.", nameof(relativePath));
        }

        var normalized = relativePath.Replace('\\', '/').TrimEnd('/');
        if (normalized is "" or "." || normalized.Split('/').Any(segment => segment is "" or "." or ".."))
        {
            throw new ArgumentException("Discovery scope contains an invalid path segment.", nameof(relativePath));
        }

        return new IntakeDiscoveryScope(IntakeDiscoveryScopeKind.DirectorySubtree, normalized);
    }

    public static IntakeDiscoveryScope DirectoryDescendants(string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);
        if (Path.IsPathRooted(relativePath))
        {
            throw new ArgumentException("Discovery scope must be relative to the supplied source.", nameof(relativePath));
        }

        if (relativePath == ".")
        {
            return new IntakeDiscoveryScope(IntakeDiscoveryScopeKind.DirectoryDescendants, ".");
        }

        var normalized = relativePath.Replace('\\', '/').TrimEnd('/');
        if (normalized is "" or "." || normalized.Split('/').Any(segment => segment is "" or "." or ".."))
        {
            throw new ArgumentException("Discovery scope contains an invalid path segment.", nameof(relativePath));
        }

        return new IntakeDiscoveryScope(IntakeDiscoveryScopeKind.DirectoryDescendants, normalized);
    }

    public static IntakeDiscoveryScope Rehydrate(IntakeDiscoveryScopeKind kind, string relativePath) =>
        kind switch
        {
            IntakeDiscoveryScopeKind.SuppliedSource when relativePath == "." => SuppliedSource(),
            IntakeDiscoveryScopeKind.DirectorySubtree => DirectorySubtree(relativePath),
            IntakeDiscoveryScopeKind.DirectoryDescendants => DirectoryDescendants(relativePath),
            _ => throw new ArgumentException("The persisted discovery scope is invalid.", nameof(relativePath))
        };
}

public sealed record IntakeDiscoveryDiagnostic
{
    public IntakeDiscoveryDiagnostic(string code, string message, IntakeDiscoveryScope scope)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Code = code;
        Message = message;
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
    }

    public string Code { get; }
    public string Message { get; }
    public IntakeDiscoveryScope Scope { get; }
}

public sealed record RecoveryRun(
    RecoveryRunId Id,
    RecoveryRunOutcome Outcome,
    string ToolIdentity,
    string ConfigurationIdentity);

public sealed record RecoveryIntakeSnapshot
{
    public RecoveryIntakeSnapshot(
        IntakeSnapshotId id,
        RecoveryRunId recoveryRunId,
        IReadOnlyList<ArtifactId> artifactIds,
        SuppliedSourceDiscoveryCompleteness discoveryCompleteness = SuppliedSourceDiscoveryCompleteness.Complete)
    {
        Id = id;
        RecoveryRunId = recoveryRunId;
        ArtifactIds = artifactIds ?? throw new ArgumentNullException(nameof(artifactIds));
        DiscoveryCompleteness = discoveryCompleteness;
    }

    public IntakeSnapshotId Id { get; }
    public RecoveryRunId RecoveryRunId { get; }
    public IReadOnlyList<ArtifactId> ArtifactIds { get; init; }
    public SuppliedSourceDiscoveryCompleteness DiscoveryCompleteness { get; }
}

public sealed record RecoveryCheckpoint(
    RecoveryCheckpointId Id,
    RecoveryRunId RecoveryRunId,
    IntakeSnapshotId? IntakeSnapshotId,
    string SubstanceHash);

public sealed record RecoveryWorkspaceState
{
    public RecoveryWorkspaceState(
        WorkspaceId workspaceId,
        RecoveryRun run,
        RecoveryIntakeSnapshot? intakeSnapshot,
        RecoveryCheckpoint checkpoint,
        IReadOnlyList<OriginalArtifact> artifacts,
        IReadOnlyList<IntakeDiscoveryDiagnostic>? discoveryDiagnostics = null)
    {
        WorkspaceId = workspaceId;
        Run = run ?? throw new ArgumentNullException(nameof(run));
        IntakeSnapshot = intakeSnapshot;
        Checkpoint = checkpoint ?? throw new ArgumentNullException(nameof(checkpoint));
        Artifacts = artifacts ?? throw new ArgumentNullException(nameof(artifacts));
        DiscoveryDiagnostics = discoveryDiagnostics ?? [];

        if (checkpoint.RecoveryRunId != run.Id || checkpoint.IntakeSnapshotId != intakeSnapshot?.Id)
        {
            throw new ArgumentException("Checkpoint associations must identify the current run and optional intake snapshot.");
        }

        if (intakeSnapshot is null)
        {
            if (run.Outcome != RecoveryRunOutcome.Blocked || Artifacts.Count != 0 ||
                !DiscoveryDiagnostics.Any(diagnostic =>
                    diagnostic.Scope.Kind == IntakeDiscoveryScopeKind.SuppliedSource ||
                    diagnostic.Scope is { Kind: IntakeDiscoveryScopeKind.DirectoryDescendants, RelativePath: "." }))
            {
                throw new ArgumentException("A snapshot-less intake state must be a root-blocked run with no admitted artifacts and a root discovery diagnostic.");
            }

            return;
        }

        if (intakeSnapshot.RecoveryRunId != run.Id ||
            !intakeSnapshot.ArtifactIds.SequenceEqual(Artifacts.Select(artifact => artifact.Id)))
        {
            throw new ArgumentException("The intake snapshot must identify the current run and admitted artifacts in deterministic order.");
        }

        if (DiscoveryDiagnostics.Any(diagnostic => diagnostic.Scope.Kind == IntakeDiscoveryScopeKind.SuppliedSource))
        {
            throw new ArgumentException("A root-scoped discovery failure cannot be represented by a successful intake snapshot.");
        }

        if (Artifacts.Count == 0 && DiscoveryDiagnostics.Any(diagnostic =>
                diagnostic.Scope is { Kind: IntakeDiscoveryScopeKind.DirectoryDescendants, RelativePath: "." }))
        {
            throw new ArgumentException("Root descendant-discovery failure without admitted artifacts cannot be represented by an incomplete empty snapshot.");
        }

        if (intakeSnapshot.DiscoveryCompleteness == SuppliedSourceDiscoveryCompleteness.Complete && DiscoveryDiagnostics.Count != 0)
        {
            throw new ArgumentException("A complete discovery snapshot cannot contain discovery-failure diagnostics.");
        }

        if (intakeSnapshot.DiscoveryCompleteness == SuppliedSourceDiscoveryCompleteness.Incomplete && DiscoveryDiagnostics.Count == 0)
        {
            throw new ArgumentException("An incomplete discovery snapshot requires at least one scoped discovery diagnostic.");
        }

        if (intakeSnapshot.DiscoveryCompleteness == SuppliedSourceDiscoveryCompleteness.Incomplete && run.Outcome != RecoveryRunOutcome.Partial)
        {
            throw new ArgumentException("An incomplete discovery snapshot requires a Partial recovery run.");
        }

        if (run.Outcome == RecoveryRunOutcome.Blocked)
        {
            throw new ArgumentException("A Blocked root discovery run cannot contain an intake snapshot.");
        }
    }

    public WorkspaceId WorkspaceId { get; }
    public RecoveryRun Run { get; }
    public RecoveryIntakeSnapshot? IntakeSnapshot { get; init; }
    public RecoveryCheckpoint Checkpoint { get; }
    public IReadOnlyList<OriginalArtifact> Artifacts { get; }
    public IReadOnlyList<IntakeDiscoveryDiagnostic> DiscoveryDiagnostics { get; }
    public bool HasSuccessfulIntakeSnapshot => IntakeSnapshot is not null;

    public OriginalArtifact GetArtifact(ArtifactId id) =>
        Artifacts.SingleOrDefault(artifact => artifact.Id == id)
        ?? throw new KeyNotFoundException($"Artifact '{id.Value}' is not present in this intake state.");
}
