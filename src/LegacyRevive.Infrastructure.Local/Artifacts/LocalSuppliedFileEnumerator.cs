using LegacyRevive.Application.Intake;

namespace LegacyRevive.Infrastructure.Local.Artifacts;

public sealed class LocalSuppliedFileEnumerator : ISuppliedFileEnumerator
{
    public Task<IReadOnlyList<SuppliedFile>> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(suppliedDirectory);
        cancellationToken.ThrowIfCancellationRequested();
        var root = Path.GetFullPath(suppliedDirectory);
        if (!Directory.Exists(root))
        {
            throw new DirectoryNotFoundException($"Supplied artifact directory '{root}' does not exist.");
        }

        IReadOnlyList<SuppliedFile> files = Directory
            .EnumerateFiles(root, "*", SearchOption.AllDirectories)
            .Select(path => new SuppliedFile(
                Path.GetRelativePath(root, path).Replace('\\', '/'),
                Path.GetFullPath(path)))
            .OrderBy(file => file.RelativePath, StringComparer.Ordinal)
            .ToArray();
        return Task.FromResult(files);
    }
}
