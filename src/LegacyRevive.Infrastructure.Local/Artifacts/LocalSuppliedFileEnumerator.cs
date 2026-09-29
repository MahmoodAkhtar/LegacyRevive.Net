using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Infrastructure.Local.Artifacts;

public class LocalSuppliedFileEnumerator : ISuppliedFileEnumerator
{
    public Task<SuppliedFileDiscoveryResult> EnumerateAsync(
        string suppliedDirectory,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(suppliedDirectory);
        cancellationToken.ThrowIfCancellationRequested();
        var root = Path.GetFullPath(suppliedDirectory);
        if (!Directory.Exists(root))
        {
            return Task.FromResult(RootFailure($"Supplied artifact directory '{root}' does not exist."));
        }

        var files = new List<SuppliedFile>();
        var diagnostics = new List<IntakeDiscoveryDiagnostic>();
        var pending = new SortedDictionary<string, string>(StringComparer.Ordinal)
        {
            ["."] = root
        };

        while (pending.Count != 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var current = pending.First();
            pending.Remove(current.Key);

            string[] directoryFiles;
            string[] childDirectories;
            try
            {
                directoryFiles = EnumerateFiles(current.Value).ToArray();
                cancellationToken.ThrowIfCancellationRequested();
                childDirectories = EnumerateDirectories(current.Value).ToArray();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                if (current.Key == ".")
                {
                    return Task.FromResult(RootFailure(exception.Message));
                }

                diagnostics.Add(new IntakeDiscoveryDiagnostic(
                    "INTAKE_DISCOVERY_FAILED",
                    exception.Message,
                    IntakeDiscoveryScope.DirectorySubtree(current.Key)));
                continue;
            }

            foreach (var path in directoryFiles.OrderBy(path => path, StringComparer.Ordinal))
            {
                cancellationToken.ThrowIfCancellationRequested();
                files.Add(new SuppliedFile(NormalizeRelativePath(root, path), Path.GetFullPath(path)));
            }

            foreach (var path in childDirectories.OrderBy(path => path, StringComparer.Ordinal))
            {
                cancellationToken.ThrowIfCancellationRequested();
                var relativePath = NormalizeRelativePath(root, path);
                pending[relativePath] = Path.GetFullPath(path);
            }
        }

        var orderedFiles = files.OrderBy(file => file.RelativePath, StringComparer.Ordinal).ToArray();
        if (diagnostics.Count == 0)
        {
            return Task.FromResult(SuppliedFileDiscoveryResult.Complete(orderedFiles));
        }

        var orderedDiagnostics = diagnostics
            .OrderBy(diagnostic => diagnostic.Scope.Kind)
            .ThenBy(diagnostic => diagnostic.Scope.RelativePath, StringComparer.Ordinal)
            .ThenBy(diagnostic => diagnostic.Code, StringComparer.Ordinal)
            .ThenBy(diagnostic => diagnostic.Message, StringComparer.Ordinal)
            .ToArray();
        return Task.FromResult(SuppliedFileDiscoveryResult.Incomplete(orderedFiles, orderedDiagnostics));
    }

    protected virtual IEnumerable<string> EnumerateFiles(string directoryPath) =>
        Directory.EnumerateFiles(directoryPath, "*", SearchOption.TopDirectoryOnly);

    protected virtual IEnumerable<string> EnumerateDirectories(string directoryPath) =>
        Directory.EnumerateDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);

    private static SuppliedFileDiscoveryResult RootFailure(string message) =>
        SuppliedFileDiscoveryResult.Incomplete(
            [],
            [new IntakeDiscoveryDiagnostic(
                "INTAKE_DISCOVERY_FAILED",
                message,
                IntakeDiscoveryScope.SuppliedSource())]);

    private static string NormalizeRelativePath(string root, string path) =>
        Path.GetRelativePath(root, Path.GetFullPath(path)).Replace('\\', '/');
}
