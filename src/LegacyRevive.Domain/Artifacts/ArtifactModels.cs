using LegacyRevive.Domain.Identity;

namespace LegacyRevive.Domain.Artifacts;

public enum ArtifactPreservationStatus
{
    Pending = 0,
    Preserved = 1,
    Failed = 2
}

public sealed record ArtifactDiagnostic(string Code, string Message);

public sealed record SuppliedArtifactProvenance(string RelativePath, string? CaptureContext)
{
    public static SuppliedArtifactProvenance Create(string relativePath, string? captureContext)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);
        if (Path.IsPathRooted(relativePath))
        {
            throw new ArgumentException("Supplied provenance must use a relative path.", nameof(relativePath));
        }

        var normalized = relativePath.Replace('\\', '/');
        if (normalized.Split('/').Any(segment => segment is "" or "." or ".."))
        {
            throw new ArgumentException("Supplied provenance contains an invalid path segment.", nameof(relativePath));
        }

        return new SuppliedArtifactProvenance(normalized, captureContext);
    }
}

public sealed record OriginalArtifact
{
    private OriginalArtifact(
        ArtifactId id,
        RecoveryRunId recoveryRunId,
        SuppliedArtifactProvenance provenance,
        Sha256Hash? contentHash,
        ArtifactPreservationStatus preservationStatus,
        string? preservedByteReference,
        ArtifactDiagnostic? diagnostic)
    {
        if (preservationStatus == ArtifactPreservationStatus.Preserved &&
            (contentHash is null || string.IsNullOrWhiteSpace(preservedByteReference)))
        {
            throw new ArgumentException("Successful preservation requires both a content hash and preserved-byte reference.");
        }

        if (preservationStatus != ArtifactPreservationStatus.Preserved && preservedByteReference is not null)
        {
            throw new ArgumentException("Only successfully preserved artifacts may expose a preserved-byte reference.");
        }

        if (preservationStatus == ArtifactPreservationStatus.Failed && diagnostic is null)
        {
            throw new ArgumentException("Failed preservation requires a scoped diagnostic.");
        }

        Id = id;
        RecoveryRunId = recoveryRunId;
        Provenance = provenance;
        ContentHash = contentHash;
        PreservationStatus = preservationStatus;
        PreservedByteReference = preservedByteReference;
        Diagnostic = diagnostic;
    }

    public ArtifactId Id { get; }
    public RecoveryRunId RecoveryRunId { get; }
    public SuppliedArtifactProvenance Provenance { get; }
    public Sha256Hash? ContentHash { get; }
    public ArtifactPreservationStatus PreservationStatus { get; }
    public string? PreservedByteReference { get; }
    public ArtifactDiagnostic? Diagnostic { get; }

    public static OriginalArtifact Pending(ArtifactId id, RecoveryRunId recoveryRunId, SuppliedArtifactProvenance provenance) =>
        new(id, recoveryRunId, provenance, null, ArtifactPreservationStatus.Pending, null, null);

    public OriginalArtifact MarkPreserved(Sha256Hash contentHash, string preservedByteReference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(preservedByteReference);
        return new OriginalArtifact(Id, RecoveryRunId, Provenance, contentHash, ArtifactPreservationStatus.Preserved, preservedByteReference, null);
    }

    public OriginalArtifact MarkFailed(ArtifactDiagnostic diagnostic, Sha256Hash? contentHash = null) =>
        new(Id, RecoveryRunId, Provenance, contentHash, ArtifactPreservationStatus.Failed, null, diagnostic ?? throw new ArgumentNullException(nameof(diagnostic)));

    public static OriginalArtifact Rehydrate(
        ArtifactId id,
        RecoveryRunId recoveryRunId,
        SuppliedArtifactProvenance provenance,
        Sha256Hash? contentHash,
        ArtifactPreservationStatus preservationStatus,
        string? preservedByteReference,
        ArtifactDiagnostic? diagnostic) =>
        new(id, recoveryRunId, provenance, contentHash, preservationStatus, preservedByteReference, diagnostic);
}
