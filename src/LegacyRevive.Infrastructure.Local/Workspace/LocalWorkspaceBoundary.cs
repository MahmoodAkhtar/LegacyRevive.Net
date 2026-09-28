using LegacyRevive.Application.Intake;

namespace LegacyRevive.Infrastructure.Local.Workspace;

public sealed class LocalWorkspaceBoundary : IWorkspaceBoundary
{
    public const string ReservedDirectoryName = ".legacyrevive";

    public LocalWorkspaceBoundary(string workspaceRoot)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workspaceRoot);
        WorkspaceRoot = Path.GetFullPath(workspaceRoot);
        InternalStateRoot = Path.Combine(WorkspaceRoot, ReservedDirectoryName);
    }

    public string WorkspaceRoot { get; }
    public string InternalStateRoot { get; }

    public Task CreateAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Directory.CreateDirectory(WorkspaceRoot);
        Directory.CreateDirectory(InternalStateRoot);
        return Task.CompletedTask;
    }

    public Task OpenAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!Directory.Exists(WorkspaceRoot) || !Directory.Exists(InternalStateRoot))
        {
            throw new DirectoryNotFoundException($"'{WorkspaceRoot}' is not a LegacyRevive recovery workspace.");
        }

        return Task.CompletedTask;
    }
}
