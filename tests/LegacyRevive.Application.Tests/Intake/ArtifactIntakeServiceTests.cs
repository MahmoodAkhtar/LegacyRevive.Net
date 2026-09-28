using System.Security.Cryptography;
using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;

namespace LegacyRevive.Application.Tests.Intake;

public sealed class ArtifactIntakeServiceTests
{
    [Fact]
    public async Task IntakeOrdersInventoryDeterministicallyAndPersistsOneCheckpointedState()
    {
        var repository = new RecordingRepository();
        var store = new MemoryArtifactStore();
        var service = CreateService(
            repository,
            store,
            new SuppliedFile("z.bin", "z"),
            new SuppliedFile("a.bin", "a"));

        var session = await service.CreateAndIntakeAsync(new IntakeRequest("source", "capture", "tool", "configuration"), TestContext.Current.CancellationToken);

        Assert.Equal(["a.bin", "z.bin"], session.State.Artifacts.Select(item => item.Provenance.RelativePath));
        Assert.Same(session.State, repository.Saved);
        Assert.Equal(session.State.Artifacts.Select(item => item.Id), session.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(session.State.Run.Id, session.State.Checkpoint.RecoveryRunId);
        Assert.Equal(session.State.IntakeSnapshot.Id, session.State.Checkpoint.IntakeSnapshotId);
    }

    [Fact]
    public async Task EquivalentSuppliedSetsInDifferentEnumerationOrdersProduceEquivalentSubstantiveOrdering()
    {
        SuppliedFile[] forward =
        [
            new("nested/z.bin", "z-content"),
            new("a.bin", "a-content"),
            new("middle.bin", "middle-content")
        ];
        var reverse = forward.Reverse().ToArray();

        var first = await CreateService(new RecordingRepository(), new MemoryArtifactStore(), forward)
            .CreateAndIntakeAsync(new IntakeRequest("source", "capture", "tool", "configuration"), TestContext.Current.CancellationToken);
        var second = await CreateService(new RecordingRepository(), new MemoryArtifactStore(), reverse)
            .CreateAndIntakeAsync(new IntakeRequest("source", "capture", "tool", "configuration"), TestContext.Current.CancellationToken);

        var firstSubstance = first.State.Artifacts.Select(artifact => (
            artifact.Provenance.RelativePath,
            artifact.Provenance.CaptureContext,
            ContentHash: artifact.ContentHash?.Value,
            artifact.PreservationStatus,
            DiagnosticCode: artifact.Diagnostic?.Code)).ToArray();
        var secondSubstance = second.State.Artifacts.Select(artifact => (
            artifact.Provenance.RelativePath,
            artifact.Provenance.CaptureContext,
            ContentHash: artifact.ContentHash?.Value,
            artifact.PreservationStatus,
            DiagnosticCode: artifact.Diagnostic?.Code)).ToArray();

        Assert.Equal(firstSubstance, secondSubstance);
        Assert.Equal(["a.bin", "middle.bin", "nested/z.bin"], firstSubstance.Select(item => item.RelativePath));
        Assert.Equal(
            first.State.IntakeSnapshot.ArtifactIds.Select(id => first.State.GetArtifact(id).Provenance.RelativePath),
            second.State.IntakeSnapshot.ArtifactIds.Select(id => second.State.GetArtifact(id).Provenance.RelativePath));
        Assert.Empty(first.State.Artifacts.Select(artifact => artifact.Id).Intersect(second.State.Artifacts.Select(artifact => artifact.Id)));
        Assert.Equal(first.State.Run.Outcome, second.State.Run.Outcome);
        Assert.Equal(first.State.Run.ToolIdentity, second.State.Run.ToolIdentity);
        Assert.Equal(first.State.Run.ConfigurationIdentity, second.State.Run.ConfigurationIdentity);
        Assert.Equal(first.State.Run.Id, first.State.Checkpoint.RecoveryRunId);
        Assert.Equal(second.State.Run.Id, second.State.Checkpoint.RecoveryRunId);
        Assert.Equal(first.State.IntakeSnapshot.Id, first.State.Checkpoint.IntakeSnapshotId);
        Assert.Equal(second.State.IntakeSnapshot.Id, second.State.Checkpoint.IntakeSnapshotId);
    }

    [Fact]
    public async Task OneArtifactFailureProducesScopedPartialStateWithoutDamagingSuccess()
    {
        var repository = new RecordingRepository();
        var store = new MemoryArtifactStore("bad.bin");
        var service = CreateService(
            repository,
            store,
            new SuppliedFile("good.bin", "good"),
            new SuppliedFile("bad.bin", "bad"));

        var session = await service.CreateAndIntakeAsync(new IntakeRequest("source"), TestContext.Current.CancellationToken);

        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);
        Assert.Equal(ArtifactPreservationStatus.Preserved, session.State.Artifacts.Single(item => item.Provenance.RelativePath == "good.bin").PreservationStatus);
        var failed = session.State.Artifacts.Single(item => item.Provenance.RelativePath == "bad.bin");
        Assert.Equal(ArtifactPreservationStatus.Failed, failed.PreservationStatus);
        Assert.Equal("TEST_FAILURE", failed.Diagnostic?.Code);
        Assert.Null(failed.PreservedByteReference);
    }

    [Fact]
    public async Task PersistenceFailureCannotBeReportedAsCompletedIntake()
    {
        var repository = new RecordingRepository { FailOnSave = true };
        var service = CreateService(repository, new MemoryArtifactStore(), new SuppliedFile("one.bin", "one"));

        await Assert.ThrowsAsync<IOException>(() => service.CreateAndIntakeAsync(new IntakeRequest("source"), TestContext.Current.CancellationToken));
        Assert.Null(repository.Saved);
    }

    [Fact]
    public async Task OpenReturnsPersistedStateWithoutGeneratingReplacementIdentities()
    {
        var repository = new RecordingRepository();
        var store = new MemoryArtifactStore();
        var service = CreateService(repository, store, new SuppliedFile("one.bin", "one"));
        var created = await service.CreateAndIntakeAsync(new IntakeRequest("source"), TestContext.Current.CancellationToken);

        var opened = await service.OpenAsync(TestContext.Current.CancellationToken);

        Assert.Same(created.State, opened.State);
        Assert.Equal(created.State.Artifacts.Single().Id, opened.State.Artifacts.Single().Id);
        Assert.Equal("one"u8.ToArray(), await opened.ReadPreservedArtifactAsync(opened.State.Artifacts.Single().Id, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task SuppliedSetIsCapturedBeforeWorkspaceInternalStateIsCreated()
    {
        var calls = new List<string>();
        var repository = new RecordingRepository();
        var service = new ArtifactIntakeService(
            new RecordingBoundary(calls),
            new RecordingEnumerator(calls),
            new MemoryArtifactStore(),
            repository);

        var session = await service.CreateAndIntakeAsync(new IntakeRequest("source"), TestContext.Current.CancellationToken);

        Assert.Equal(["enumerate", "create"], calls);
        Assert.Equal(Path.GetFullPath("source"), session.State.Artifacts.Single().Provenance.CaptureContext);
    }

    private static ArtifactIntakeService CreateService(
        RecordingRepository repository,
        MemoryArtifactStore store,
        params SuppliedFile[] files) =>
        new(new MemoryBoundary(), new FixedEnumerator(files), store, repository);

    private sealed class MemoryBoundary : IWorkspaceBoundary
    {
        public string WorkspaceRoot => "workspace";
        public string InternalStateRoot => "workspace/.legacyrevive";
        public Task CreateAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task OpenAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }

    private sealed class RecordingBoundary(List<string> calls) : IWorkspaceBoundary
    {
        public string WorkspaceRoot => "workspace";
        public string InternalStateRoot => "workspace/.legacyrevive";
        public Task CreateAsync(CancellationToken cancellationToken = default)
        {
            calls.Add("create");
            return Task.CompletedTask;
        }

        public Task OpenAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }

    private sealed class RecordingEnumerator(List<string> calls) : ISuppliedFileEnumerator
    {
        public Task<IReadOnlyList<SuppliedFile>> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default)
        {
            calls.Add("enumerate");
            return Task.FromResult<IReadOnlyList<SuppliedFile>>([new SuppliedFile("one.bin", "one")]);
        }
    }

    private sealed class FixedEnumerator(IReadOnlyList<SuppliedFile> files) : ISuppliedFileEnumerator
    {
        public Task<IReadOnlyList<SuppliedFile>> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default) =>
            Task.FromResult(files);
    }

    private sealed class MemoryArtifactStore(string? failingPath = null) : IOriginalArtifactStore
    {
        private readonly Dictionary<string, byte[]> _content = new(StringComparer.Ordinal);

        public Task<ArtifactPreservationResult> PreserveAsync(SuppliedFile suppliedFile, ArtifactId artifactId, CancellationToken cancellationToken = default)
        {
            if (suppliedFile.RelativePath == failingPath)
            {
                return Task.FromResult(ArtifactPreservationResult.Failure(new ArtifactDiagnostic("TEST_FAILURE", "Injected failure.")));
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(suppliedFile.SourcePath);
            var reference = $".legacyrevive/artifacts/{artifactId.Value:D}.bin";
            _content[reference] = bytes;
            return Task.FromResult(ArtifactPreservationResult.Success(
                Sha256Hash.FromHex(Convert.ToHexStringLower(SHA256.HashData(bytes))),
                reference));
        }

        public Task<Stream> OpenReadAsync(string preservedByteReference, CancellationToken cancellationToken = default) =>
            Task.FromResult<Stream>(new MemoryStream(_content[preservedByteReference], writable: false));
    }

    private sealed class RecordingRepository : IIntakeStateRepository
    {
        public bool FailOnSave { get; init; }
        public RecoveryWorkspaceState? Saved { get; private set; }

        public Task InitializeAsync(WorkspaceId workspaceId, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task SaveAsync(RecoveryWorkspaceState state, CancellationToken cancellationToken = default)
        {
            if (FailOnSave)
            {
                throw new IOException("Injected persistence failure.");
            }

            Saved = state;
            return Task.CompletedTask;
        }

        public Task<RecoveryWorkspaceState> LoadCurrentAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(Saved ?? throw new InvalidOperationException("No state was saved."));
    }
}
