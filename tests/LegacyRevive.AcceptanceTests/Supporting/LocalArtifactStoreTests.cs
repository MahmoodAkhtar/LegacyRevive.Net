using System.Security.Cryptography;
using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Infrastructure.Local.Artifacts;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.AcceptanceTests.Supporting;

public sealed class LocalArtifactStoreTests
{
    [Fact]
    public async Task SnapshotCopiesExactBytesVerifiesHashAndCreatesReadOnlyWorkspaceOwnedFile()
    {
        using var test = new TestWorkspace();
        var sourceDirectory = test.CreateDirectory("source");
        var sourcePath = Path.Combine(sourceDirectory, "input.bin");
        var bytes = Enumerable.Range(0, 8193).Select(index => (byte)(index % 251)).ToArray();
        await File.WriteAllBytesAsync(sourcePath, bytes, TestContext.Current.CancellationToken);
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var store = new LocalOriginalArtifactStore(boundary);

        var result = await store.PreserveAsync(new SuppliedFile("input.bin", sourcePath), ArtifactId.Create(), TestContext.Current.CancellationToken);

        Assert.True(result.Succeeded);
        Assert.Equal(Convert.ToHexStringLower(SHA256.HashData(bytes)), result.ContentHash?.Value);
        Assert.StartsWith(".legacyrevive/", result.PreservedByteReference, StringComparison.Ordinal);
        var storedPath = Path.Combine(boundary.WorkspaceRoot, result.PreservedByteReference!.Replace('/', Path.DirectorySeparatorChar));
        Assert.Equal(bytes, await File.ReadAllBytesAsync(storedPath, TestContext.Current.CancellationToken));
        Assert.True(File.GetAttributes(storedPath).HasFlag(FileAttributes.ReadOnly));
    }

    [Fact]
    public async Task IntegrityVerificationFailureDoesNotPublishSnapshotAndCleansTemporaryFile()
    {
        using var test = new TestWorkspace();
        var sourceDirectory = test.CreateDirectory("source");
        var sourcePath = Path.Combine(sourceDirectory, "input.bin");
        await File.WriteAllTextAsync(sourcePath, "integrity failure", TestContext.Current.CancellationToken);
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var store = new FailingVerificationStore(boundary);

        var result = await store.PreserveAsync(new SuppliedFile("input.bin", sourcePath), ArtifactId.Create(), TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal("INTAKE_INTEGRITY_VERIFICATION_FAILED", result.Diagnostic?.Code);
        Assert.Null(result.PreservedByteReference);
        Assert.Empty(Directory.EnumerateFiles(Path.Combine(boundary.InternalStateRoot, "artifacts")));
    }

    [Fact]
    public async Task SnapshotReadFailureIsExplicitAndDoesNotLeavePartialFile()
    {
        using var test = new TestWorkspace();
        var sourceDirectory = test.CreateDirectory("source-directory-used-as-file");
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var store = new LocalOriginalArtifactStore(boundary);

        var result = await store.PreserveAsync(new SuppliedFile("not-a-file", sourceDirectory), ArtifactId.Create(), TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal("INTAKE_PRESERVATION_FAILED", result.Diagnostic?.Code);
        Assert.Null(result.PreservedByteReference);
        Assert.Empty(Directory.EnumerateFiles(Path.Combine(boundary.InternalStateRoot, "artifacts")));
    }

    [Fact]
    public async Task ReadRejectsReferencesOutsideArtifactStore()
    {
        using var test = new TestWorkspace();
        var boundary = new LocalWorkspaceBoundary(Path.Combine(test.Root, "workspace"));
        await boundary.CreateAsync(TestContext.Current.CancellationToken);
        var store = new LocalOriginalArtifactStore(boundary);

        await Assert.ThrowsAsync<InvalidDataException>(() => store.OpenReadAsync("../outside.bin", TestContext.Current.CancellationToken));
    }

    private sealed class FailingVerificationStore(LocalWorkspaceBoundary boundary) : LocalOriginalArtifactStore(boundary)
    {
        protected override Task<bool> VerifySnapshotAsync(string temporaryPath, Sha256Hash expectedHash, CancellationToken cancellationToken) =>
            Task.FromResult(false);
    }
}
