using System.Security.Cryptography;
using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.Infrastructure.Local.Artifacts;

public class LocalOriginalArtifactStore : IOriginalArtifactStore
{
    private readonly string _workspaceRoot;
    private readonly string _artifactRoot;

    public LocalOriginalArtifactStore(LocalWorkspaceBoundary workspaceBoundary)
    {
        ArgumentNullException.ThrowIfNull(workspaceBoundary);
        _workspaceRoot = workspaceBoundary.WorkspaceRoot;
        _artifactRoot = Path.Combine(workspaceBoundary.InternalStateRoot, "artifacts");
    }

    public async Task<ArtifactPreservationResult> PreserveAsync(
        SuppliedFile suppliedFile,
        ArtifactId artifactId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(suppliedFile);
        Directory.CreateDirectory(_artifactRoot);
        var destination = Path.Combine(_artifactRoot, $"{artifactId.Value:D}.bin");
        var temporary = Path.Combine(_artifactRoot, $".pending-{artifactId.Value:D}-{Guid.NewGuid():N}");
        Sha256Hash? suppliedHash = null;

        try
        {
            await using (var source = new FileStream(
                             suppliedFile.SourcePath,
                             FileMode.Open,
                             FileAccess.Read,
                             FileShare.Read,
                             81920,
                             FileOptions.Asynchronous | FileOptions.SequentialScan))
            await using (var target = new FileStream(
                             temporary,
                             FileMode.CreateNew,
                             FileAccess.Write,
                             FileShare.None,
                             81920,
                             FileOptions.Asynchronous | FileOptions.SequentialScan))
            using (var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256))
            {
                var buffer = new byte[81920];
                int read;
                while ((read = await source.ReadAsync(buffer, cancellationToken)) != 0)
                {
                    await target.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                    hasher.AppendData(buffer, 0, read);
                }

                await target.FlushAsync(cancellationToken);
                target.Flush(flushToDisk: true);
                suppliedHash = Sha256Hash.FromHex(Convert.ToHexStringLower(hasher.GetHashAndReset()));
            }

            if (!await VerifySnapshotAsync(temporary, suppliedHash.Value, cancellationToken))
            {
                return ArtifactPreservationResult.Failure(
                    new ArtifactDiagnostic("INTAKE_INTEGRITY_VERIFICATION_FAILED", $"Preserved bytes for '{suppliedFile.RelativePath}' did not match their SHA-256 identity."),
                    suppliedHash);
            }

            File.Move(temporary, destination);
            File.SetAttributes(destination, File.GetAttributes(destination) | FileAttributes.ReadOnly);
            var reference = Path.GetRelativePath(_workspaceRoot, destination).Replace('\\', '/');
            return ArtifactPreservationResult.Success(suppliedHash.Value, reference);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            return ArtifactPreservationResult.Failure(
                new ArtifactDiagnostic("INTAKE_PRESERVATION_FAILED", $"Could not preserve '{suppliedFile.RelativePath}': {exception.Message}"),
                suppliedHash);
        }
        finally
        {
            if (File.Exists(temporary))
            {
                File.Delete(temporary);
            }
        }
    }

    public Task<Stream> OpenReadAsync(string preservedByteReference, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(preservedByteReference);
        cancellationToken.ThrowIfCancellationRequested();
        var path = ResolvePreservedPath(preservedByteReference);
        Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, FileOptions.Asynchronous | FileOptions.SequentialScan);
        return Task.FromResult(stream);
    }

    protected virtual async Task<bool> VerifySnapshotAsync(string temporaryPath, Sha256Hash expectedHash, CancellationToken cancellationToken)
    {
        await using var stored = new FileStream(temporaryPath, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, FileOptions.Asynchronous | FileOptions.SequentialScan);
        var actual = Sha256Hash.FromHex(Convert.ToHexStringLower(await SHA256.HashDataAsync(stored, cancellationToken)));
        return actual == expectedHash;
    }

    private string ResolvePreservedPath(string reference)
    {
        if (Path.IsPathRooted(reference))
        {
            throw new InvalidDataException("Preserved-byte references must be workspace-relative.");
        }

        var fullPath = Path.GetFullPath(Path.Combine(_workspaceRoot, reference.Replace('/', Path.DirectorySeparatorChar)));
        var artifactRootWithSeparator = _artifactRoot.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        if (!fullPath.StartsWith(artifactRootWithSeparator, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidDataException("Preserved-byte reference escapes the workspace artifact store.");
        }

        return fullPath;
    }
}
