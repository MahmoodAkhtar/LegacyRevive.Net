using System.Security.Cryptography;
using System.Text;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Application.Intake;

public sealed record IntakeRequest(
    string SuppliedDirectory,
    string? CaptureContext = null,
    string ToolIdentity = "LegacyRevive.NET",
    string ConfigurationIdentity = "DEV-SPEC-001/default");

public sealed class ArtifactIntakeService(
    IWorkspaceBoundary workspaceBoundary,
    ISuppliedFileEnumerator suppliedFileEnumerator,
    IOriginalArtifactStore artifactStore,
    IIntakeStateRepository stateRepository)
{
    public async Task<RecoveryWorkspaceSession> CreateAndIntakeAsync(IntakeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.SuppliedDirectory);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.ToolIdentity);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.ConfigurationIdentity);

        // Capture the supplied set before creating internal workspace files. This also keeps
        // a developer-selected source/workspace root from admitting .legacyrevive itself.
        var discovery = await suppliedFileEnumerator.EnumerateAsync(request.SuppliedDirectory, cancellationToken);
        await workspaceBoundary.CreateAsync(cancellationToken);
        var workspaceId = WorkspaceId.Create();
        await stateRepository.InitializeAsync(workspaceId, cancellationToken);

        var runId = RecoveryRunId.Create();
        var captureContext = request.CaptureContext ?? Path.GetFullPath(request.SuppliedDirectory);
        var artifacts = new List<OriginalArtifact>(discovery.Files.Count);

        foreach (var suppliedFile in discovery.Files.OrderBy(file => file.RelativePath, StringComparer.Ordinal))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var artifact = OriginalArtifact.Pending(
                ArtifactId.Create(),
                runId,
                SuppliedArtifactProvenance.Create(suppliedFile.RelativePath, captureContext));

            ArtifactPreservationResult result;
            try
            {
                result = await artifactStore.PreserveAsync(suppliedFile, artifact.Id, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                result = ArtifactPreservationResult.Failure(
                    new ArtifactDiagnostic("INTAKE_PRESERVATION_FAILED", exception.Message));
            }

            artifacts.Add(result.Succeeded
                ? artifact.MarkPreserved(
                    result.ContentHash ?? throw new InvalidOperationException("A successful preservation result must include a hash."),
                    result.PreservedByteReference ?? throw new InvalidOperationException("A successful preservation result must include a byte reference."))
                : artifact.MarkFailed(
                    result.Diagnostic ?? new ArtifactDiagnostic("INTAKE_PRESERVATION_FAILED", "Preservation failed without adapter detail."),
                    result.ContentHash));
        }

        var rootDescendantDiscoveryWithoutFiles =
            discovery.Files.Count == 0 &&
            discovery.Diagnostics.Any(diagnostic =>
                diagnostic.Scope is { Kind: IntakeDiscoveryScopeKind.DirectoryDescendants, RelativePath: "." });
        var hasNoTrustworthyRootArtifactSet = discovery.IsRootBlocked || rootDescendantDiscoveryWithoutFiles;
        var outcome = hasNoTrustworthyRootArtifactSet
            ? RecoveryRunOutcome.Blocked
            : discovery.Completeness == SuppliedSourceDiscoveryCompleteness.Incomplete ||
              artifacts.Any(artifact => artifact.PreservationStatus == ArtifactPreservationStatus.Failed)
                ? RecoveryRunOutcome.Partial
                : RecoveryRunOutcome.Completed;
        var run = new RecoveryRun(runId, outcome, request.ToolIdentity, request.ConfigurationIdentity);
        var diagnostics = discovery.Diagnostics
            .OrderBy(diagnostic => diagnostic.Scope.Kind)
            .ThenBy(diagnostic => diagnostic.Scope.RelativePath, StringComparer.Ordinal)
            .ThenBy(diagnostic => diagnostic.Code, StringComparer.Ordinal)
            .ThenBy(diagnostic => diagnostic.Message, StringComparer.Ordinal)
            .ToArray();
        var snapshot = hasNoTrustworthyRootArtifactSet
            ? null
            : new RecoveryIntakeSnapshot(
                IntakeSnapshotId.Create(),
                runId,
                artifacts.Select(artifact => artifact.Id).ToArray(),
                discovery.Completeness);
        var checkpoint = new RecoveryCheckpoint(
            RecoveryCheckpointId.Create(),
            runId,
            snapshot?.Id,
            ComputeSubstanceHash(run, snapshot, diagnostics, artifacts));
        var state = new RecoveryWorkspaceState(workspaceId, run, snapshot, checkpoint, artifacts, diagnostics);

        await stateRepository.SaveAsync(state, cancellationToken);
        return new RecoveryWorkspaceSession(state, artifactStore);
    }

    public async Task<RecoveryWorkspaceSession> OpenAsync(CancellationToken cancellationToken = default)
    {
        await workspaceBoundary.OpenAsync(cancellationToken);
        return new RecoveryWorkspaceSession(await stateRepository.LoadCurrentAsync(cancellationToken), artifactStore);
    }

    private static string ComputeSubstanceHash(
        RecoveryRun run,
        RecoveryIntakeSnapshot? snapshot,
        IEnumerable<IntakeDiscoveryDiagnostic> discoveryDiagnostics,
        IEnumerable<OriginalArtifact> artifacts)
    {
        var builder = new StringBuilder();
        builder.Append(run.ToolIdentity).Append('\n').Append(run.ConfigurationIdentity).Append('\n').Append(run.Outcome).Append('\n');
        builder.Append(snapshot is null ? "NO_SNAPSHOT" : snapshot.DiscoveryCompleteness).Append('\n');
        foreach (var diagnostic in discoveryDiagnostics
                     .OrderBy(item => item.Scope.Kind)
                     .ThenBy(item => item.Scope.RelativePath, StringComparer.Ordinal)
                     .ThenBy(item => item.Code, StringComparer.Ordinal)
                     .ThenBy(item => item.Message, StringComparer.Ordinal))
        {
            builder
                .Append(diagnostic.Scope.Kind).Append('|')
                .Append(diagnostic.Scope.RelativePath).Append('|')
                .Append(diagnostic.Code).Append('|')
                .Append(diagnostic.Message).Append('\n');
        }

        foreach (var artifact in artifacts.OrderBy(item => item.Provenance.RelativePath, StringComparer.Ordinal))
        {
            builder
                .Append(artifact.Provenance.RelativePath).Append('|')
                .Append(artifact.Provenance.CaptureContext).Append('|')
                .Append(artifact.ContentHash?.Value).Append('|')
                .Append(artifact.PreservationStatus).Append('|')
                .Append(artifact.Diagnostic?.Code).Append('|')
                .Append(artifact.Diagnostic?.Message).Append('\n');
        }

        return Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(builder.ToString())));
    }
}

public sealed class RecoveryWorkspaceSession(RecoveryWorkspaceState state, IOriginalArtifactStore artifactStore)
{
    public RecoveryWorkspaceState State { get; } = state;

    public async Task<byte[]> ReadPreservedArtifactAsync(ArtifactId artifactId, CancellationToken cancellationToken = default)
    {
        var artifact = State.GetArtifact(artifactId);
        if (artifact.PreservationStatus != ArtifactPreservationStatus.Preserved ||
            artifact.ContentHash is null ||
            artifact.PreservedByteReference is null)
        {
            throw new InvalidOperationException("The artifact has no successfully preserved byte representation.");
        }

        await using var stream = await artifactStore.OpenReadAsync(artifact.PreservedByteReference, cancellationToken);
        using var memory = new MemoryStream();
        await stream.CopyToAsync(memory, cancellationToken);
        var bytes = memory.ToArray();
        var actualHash = Sha256Hash.FromHex(Convert.ToHexStringLower(SHA256.HashData(bytes)));
        if (actualHash != artifact.ContentHash.Value)
        {
            throw new InvalidDataException($"Preserved bytes for artifact '{artifact.Id.Value}' failed SHA-256 verification.");
        }

        return bytes;
    }
}
