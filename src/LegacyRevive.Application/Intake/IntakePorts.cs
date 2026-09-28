using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Application.Intake;

public sealed record SuppliedFile(string RelativePath, string SourcePath);

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
    Task<IReadOnlyList<SuppliedFile>> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default);
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
