using LegacyRevive.Application.Intake;
using LegacyRevive.Infrastructure.Local.Artifacts;
using LegacyRevive.Infrastructure.Local.Persistence;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.Infrastructure.Local;

public sealed class LocalRecoveryWorkspaceApplication
{
    private readonly ArtifactIntakeService _intakeService;

    public LocalRecoveryWorkspaceApplication(string workspaceRoot)
    {
        var boundary = new LocalWorkspaceBoundary(workspaceRoot);
        _intakeService = new ArtifactIntakeService(
            boundary,
            new LocalSuppliedFileEnumerator(),
            new LocalOriginalArtifactStore(boundary),
            new SqliteIntakeStateRepository(boundary));
    }

    public Task<RecoveryWorkspaceSession> CreateAndIntakeAsync(IntakeRequest request, CancellationToken cancellationToken = default) =>
        _intakeService.CreateAndIntakeAsync(request, cancellationToken);

    public Task<RecoveryWorkspaceSession> OpenAsync(CancellationToken cancellationToken = default) =>
        _intakeService.OpenAsync(cancellationToken);
}
