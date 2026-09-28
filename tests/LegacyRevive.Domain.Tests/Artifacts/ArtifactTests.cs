using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;

namespace LegacyRevive.Domain.Tests.Artifacts;

public sealed class ArtifactTests
{
    [Fact]
    public void IdentityHashPathAndStorageReferenceRemainDistinct()
    {
        var runId = RecoveryRunId.Create();
        var artifact = OriginalArtifact.Pending(
                ArtifactId.Create(),
                runId,
                SuppliedArtifactProvenance.Create("bin/legacy.dll", "captured from release media"))
            .MarkPreserved(
                Sha256Hash.FromHex(new string('a', 64)),
                ".legacyrevive/artifacts/stored.bin");

        Assert.NotEqual(artifact.Id.Value.ToString("N"), artifact.ContentHash!.Value.Value);
        Assert.NotEqual(artifact.Provenance.RelativePath, artifact.PreservedByteReference);
        Assert.Equal("bin/legacy.dll", artifact.Provenance.RelativePath);
        Assert.Equal("captured from release media", artifact.Provenance.CaptureContext);
    }

    [Fact]
    public void EqualHashesDoNotImplyEqualArtifactIdentities()
    {
        var runId = RecoveryRunId.Create();
        var hash = Sha256Hash.FromHex(new string('b', 64));
        var first = OriginalArtifact.Pending(ArtifactId.Create(), runId, SuppliedArtifactProvenance.Create("a.bin", null))
            .MarkPreserved(hash, ".legacyrevive/artifacts/a.bin");
        var second = OriginalArtifact.Pending(ArtifactId.Create(), runId, SuppliedArtifactProvenance.Create("b.bin", null))
            .MarkPreserved(hash, ".legacyrevive/artifacts/b.bin");

        Assert.Equal(first.ContentHash, second.ContentHash);
        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void PreservationCannotSucceedWithoutHashAndWorkspaceByteReference()
    {
        var artifact = OriginalArtifact.Pending(
            ArtifactId.Create(),
            RecoveryRunId.Create(),
            SuppliedArtifactProvenance.Create("legacy.bin", null));

        Assert.Equal(ArtifactPreservationStatus.Pending, artifact.PreservationStatus);
        Assert.Throws<ArgumentException>(() => artifact.MarkPreserved(Sha256Hash.FromHex(new string('c', 64)), ""));
        Assert.Throws<ArgumentException>(() => OriginalArtifact.Rehydrate(
            artifact.Id,
            artifact.RecoveryRunId,
            artifact.Provenance,
            null,
            ArtifactPreservationStatus.Preserved,
            ".legacyrevive/artifacts/x.bin",
            null));
    }

    [Fact]
    public void FailedPreservationRequiresDiagnosticAndCannotExposeStoredBytes()
    {
        var artifact = OriginalArtifact.Pending(
            ArtifactId.Create(),
            RecoveryRunId.Create(),
            SuppliedArtifactProvenance.Create("legacy.bin", null));
        var failed = artifact.MarkFailed(new ArtifactDiagnostic("READ_FAILED", "Access denied."));

        Assert.Equal(ArtifactPreservationStatus.Failed, failed.PreservationStatus);
        Assert.Null(failed.PreservedByteReference);
        Assert.NotNull(failed.Diagnostic);
        Assert.Throws<ArgumentException>(() => OriginalArtifact.Rehydrate(
            artifact.Id,
            artifact.RecoveryRunId,
            artifact.Provenance,
            null,
            ArtifactPreservationStatus.Failed,
            ".legacyrevive/artifacts/x.bin",
            new ArtifactDiagnostic("READ_FAILED", "Access denied.")));
    }
}
