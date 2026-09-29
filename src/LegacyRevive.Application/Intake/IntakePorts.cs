using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Application.Intake;

public sealed record SuppliedFile(string RelativePath, string SourcePath);

public sealed record SuppliedFileDiscoveryResult
{
    public SuppliedFileDiscoveryResult(
        IReadOnlyList<SuppliedFile> files,
        SuppliedSourceDiscoveryCompleteness completeness,
        IReadOnlyList<IntakeDiscoveryDiagnostic>? diagnostics = null)
    {
        Files = files ?? throw new ArgumentNullException(nameof(files));
        Completeness = completeness;
        Diagnostics = diagnostics ?? [];

        if (completeness == SuppliedSourceDiscoveryCompleteness.Complete && Diagnostics.Count != 0)
        {
            throw new ArgumentException("Complete discovery cannot contain discovery-failure diagnostics.", nameof(diagnostics));
        }

        if (completeness == SuppliedSourceDiscoveryCompleteness.Incomplete && Diagnostics.Count == 0)
        {
            throw new ArgumentException("Incomplete discovery requires at least one scoped diagnostic.", nameof(diagnostics));
        }

        if (IsRootBlocked && Files.Count != 0)
        {
            throw new ArgumentException("Root-blocked discovery cannot establish an admitted supplied-file set.", nameof(files));
        }
    }

    public IReadOnlyList<SuppliedFile> Files { get; }
    public SuppliedSourceDiscoveryCompleteness Completeness { get; }
    public IReadOnlyList<IntakeDiscoveryDiagnostic> Diagnostics { get; }
    public bool IsRootBlocked => Diagnostics.Any(diagnostic => diagnostic.Scope.Kind == IntakeDiscoveryScopeKind.SuppliedSource);

    public static SuppliedFileDiscoveryResult Complete(IReadOnlyList<SuppliedFile> files) =>
        new(files, SuppliedSourceDiscoveryCompleteness.Complete);

    public static SuppliedFileDiscoveryResult Incomplete(
        IReadOnlyList<SuppliedFile> files,
        IReadOnlyList<IntakeDiscoveryDiagnostic> diagnostics) =>
        new(files, SuppliedSourceDiscoveryCompleteness.Incomplete, diagnostics);
}

public sealed record ArtifactPreservationResult
{
    private ArtifactPreservationResult(
        bool succeeded,
        Sha256Hash? contentHash,
        string? preservedByteReference,
        ArtifactDiagnostic? diagnostic)
    {
        Succeeded = succeeded;
        ContentHash = contentHash;
        PreservedByteReference = preservedByteReference;
        Diagnostic = diagnostic;
    }

    public bool Succeeded { get; }
    public Sha256Hash? ContentHash { get; }
    public string? PreservedByteReference { get; }
    public ArtifactDiagnostic? Diagnostic { get; }

    public static ArtifactPreservationResult Success(Sha256Hash contentHash, string preservedByteReference) =>
        new(true, contentHash, preservedByteReference, null);

    public static ArtifactPreservationResult Failure(ArtifactDiagnostic diagnostic, Sha256Hash? contentHash = null) =>
        new(false, contentHash, null, diagnostic);
}

public interface IWorkspaceBoundary
{
    string WorkspaceRoot { get; }
    string InternalStateRoot { get; }
    Task CreateAsync(CancellationToken cancellationToken = default);
    Task OpenAsync(CancellationToken cancellationToken = default);
}

public interface ISuppliedFileEnumerator
{
    Task<SuppliedFileDiscoveryResult> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default);
}

public interface IOriginalArtifactStore
{
    Task<ArtifactPreservationResult> PreserveAsync(SuppliedFile suppliedFile, ArtifactId artifactId, CancellationToken cancellationToken = default);
    Task<Stream> OpenReadAsync(string preservedByteReference, CancellationToken cancellationToken = default);
}

public interface IIntakeStateRepository
{
    Task InitializeAsync(WorkspaceId workspaceId, CancellationToken cancellationToken = default);
    Task SaveAsync(RecoveryWorkspaceState state, CancellationToken cancellationToken = default);
    Task<RecoveryWorkspaceState> LoadCurrentAsync(CancellationToken cancellationToken = default);
}
