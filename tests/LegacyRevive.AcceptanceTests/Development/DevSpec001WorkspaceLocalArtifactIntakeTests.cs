using System.Security.Cryptography;
using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local;
using LegacyRevive.Infrastructure.Local.Artifacts;
using LegacyRevive.Infrastructure.Local.Persistence;
using LegacyRevive.Infrastructure.Local.Workspace;

namespace LegacyRevive.AcceptanceTests.Development;

public sealed class DevSpec001WorkspaceLocalArtifactIntakeTests
{
    [Fact]
    public async Task S1_CreateWorkspaceAndPreserveSmallLocalTree()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        Directory.CreateDirectory(Path.Combine(source, "nested"));
        await File.WriteAllTextAsync(Path.Combine(source, "one.dll"), "one", TestContext.Current.CancellationToken);
        await File.WriteAllBytesAsync(Path.Combine(source, "nested", "two.bin"), [0, 1, 2, 3], TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");

        var session = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source, "release-media-01", "test-tool", "test-configuration"), TestContext.Current.CancellationToken);

        Assert.True(Directory.Exists(Path.Combine(workspace, ".legacyrevive")));
        Assert.Equal(2, session.State.Artifacts.Count);
        Assert.Equal(2, session.State.IntakeSnapshot!.ArtifactIds.Count);
        Assert.All(session.State.Artifacts, artifact =>
        {
            Assert.Equal(ArtifactPreservationStatus.Preserved, artifact.PreservationStatus);
            Assert.NotNull(artifact.ContentHash);
            Assert.Equal("release-media-01", artifact.Provenance.CaptureContext);
            Assert.NotNull(artifact.PreservedByteReference);
            Assert.StartsWith(".legacyrevive/", artifact.PreservedByteReference, StringComparison.Ordinal);
            Assert.True(File.Exists(Path.Combine(workspace, artifact.PreservedByteReference!.Replace('/', Path.DirectorySeparatorChar))));
        });
        Assert.NotEqual(Guid.Empty, session.State.Run.Id.Value);
        Assert.NotEqual(Guid.Empty, session.State.IntakeSnapshot.Id.Value);
        Assert.NotEqual(Guid.Empty, session.State.Checkpoint.Id.Value);
        Assert.Equal(64, session.State.Checkpoint.SubstanceHash.Length);
        Assert.Equal(["nested/two.bin", "one.dll"], session.State.Artifacts.Select(item => item.Provenance.RelativePath));
        Assert.Empty(Directory.EnumerateFiles(workspace, "*", SearchOption.TopDirectoryOnly));
    }

    [Fact]
    public async Task S2_ReopenAfterDeletingOriginalSuppliedDirectory()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var expected = "workspace-owned bytes"u8.ToArray();
        await File.WriteAllBytesAsync(Path.Combine(source, "legacy.dat"), expected, TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var first = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source, source), TestContext.Current.CancellationToken);
        var firstArtifact = Assert.Single(first.State.Artifacts);
        Directory.Delete(source, recursive: true);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);
        var reopenedArtifact = Assert.Single(reopened.State.Artifacts);

        Assert.Equal(firstArtifact.Id, reopenedArtifact.Id);
        Assert.Equal(firstArtifact.ContentHash, reopenedArtifact.ContentHash);
        Assert.Equal(firstArtifact.Provenance, reopenedArtifact.Provenance);
        Assert.Equal(first.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(expected, await reopened.ReadPreservedArtifactAsync(reopenedArtifact.Id, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task S3_DistinctSuppliedFilesWithIdenticalContent()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "a.bin"), "identical", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "b.bin"), "identical", TestContext.Current.CancellationToken);

        var session = await new LocalRecoveryWorkspaceApplication(Path.Combine(test.Root, "workspace"))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        var artifacts = session.State.Artifacts.OrderBy(item => item.Provenance.RelativePath).ToArray();
        Assert.Equal(artifacts[0].ContentHash, artifacts[1].ContentHash);
        Assert.NotEqual(artifacts[0].Id, artifacts[1].Id);
        Assert.Equal("a.bin", artifacts[0].Provenance.RelativePath);
        Assert.Equal("b.bin", artifacts[1].Provenance.RelativePath);
    }

    [Fact]
    public async Task S4_UnsupportedButReadableFileIsPreservedWithoutSemanticConclusions()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var expected = "unknown format"u8.ToArray();
        await File.WriteAllBytesAsync(Path.Combine(source, "opaque.unrecognized"), expected, TestContext.Current.CancellationToken);

        var session = await new LocalRecoveryWorkspaceApplication(Path.Combine(test.Root, "workspace"))
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        var artifact = Assert.Single(session.State.Artifacts);

        Assert.Equal("opaque.unrecognized", artifact.Provenance.RelativePath);
        Assert.Equal(ArtifactPreservationStatus.Preserved, artifact.PreservationStatus);
        Assert.Equal(expected, await session.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        Assert.Null(artifact.Diagnostic);
    }

    [Fact]
    public async Task S5_ControlledPreservationFailureIsScopedAndPartialProgressPersists()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "good.bin"), "good", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "fail.bin"), "fail", TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var boundary = new LocalWorkspaceBoundary(workspace);
        var realStore = new LocalOriginalArtifactStore(boundary);
        var service = new ArtifactIntakeService(
            boundary,
            new DeleteAfterEnumerationEnumerator(new LocalSuppliedFileEnumerator(), "fail.bin"),
            realStore,
            new SqliteIntakeStateRepository(boundary));

        var session = await service.CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        var failed = session.State.Artifacts.Single(item => item.Provenance.RelativePath == "fail.bin");
        var succeeded = session.State.Artifacts.Single(item => item.Provenance.RelativePath == "good.bin");

        Assert.Equal(ArtifactPreservationStatus.Failed, failed.PreservationStatus);
        Assert.Null(failed.PreservedByteReference);
        Assert.Equal("INTAKE_PRESERVATION_FAILED", failed.Diagnostic?.Code);
        Assert.Contains("fail.bin", failed.Diagnostic!.Message, StringComparison.Ordinal);
        Assert.Equal(ArtifactPreservationStatus.Preserved, succeeded.PreservationStatus);
        Assert.Null(succeeded.Diagnostic);
        Assert.Equal("good"u8.ToArray(), await session.ReadPreservedArtifactAsync(succeeded.Id, TestContext.Current.CancellationToken));
        Assert.Equal(RecoveryRunOutcome.Partial, session.State.Run.Outcome);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);
        var reopenedFailed = reopened.State.Artifacts.Single(item => item.Provenance.RelativePath == "fail.bin");
        var reopenedSucceeded = reopened.State.Artifacts.Single(item => item.Provenance.RelativePath == "good.bin");

        Assert.Equal(session.State.WorkspaceId, reopened.State.WorkspaceId);
        Assert.Equal(session.State.Run, reopened.State.Run);
        Assert.Equal(session.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(2, reopened.State.Artifacts.Count);
        Assert.Equal(failed.Id, reopenedFailed.Id);
        Assert.Equal(ArtifactPreservationStatus.Failed, reopenedFailed.PreservationStatus);
        Assert.Equal(failed.Diagnostic, reopenedFailed.Diagnostic);
        Assert.Equal("INTAKE_PRESERVATION_FAILED", reopenedFailed.Diagnostic?.Code);
        Assert.Contains("fail.bin", reopenedFailed.Diagnostic!.Message, StringComparison.Ordinal);
        Assert.Null(reopenedFailed.PreservedByteReference);
        Assert.Equal(succeeded, reopenedSucceeded);
        Assert.Equal(ArtifactPreservationStatus.Preserved, reopenedSucceeded.PreservationStatus);
        Assert.NotNull(reopenedSucceeded.PreservedByteReference);
        Assert.Equal("good"u8.ToArray(), await reopened.ReadPreservedArtifactAsync(reopenedSucceeded.Id, TestContext.Current.CancellationToken));
        Assert.Equal(RecoveryRunOutcome.Partial, reopened.State.Run.Outcome);
        Assert.Equal(reopened.State.Run.Id, reopened.State.IntakeSnapshot!.RecoveryRunId);
        Assert.Equal(reopened.State.Run.Id, reopened.State.Checkpoint.RecoveryRunId);
        Assert.Equal(reopened.State.IntakeSnapshot.Id, reopened.State.Checkpoint.IntakeSnapshotId);
        Assert.Equal(reopened.State.Artifacts.Select(artifact => artifact.Id), reopened.State.IntakeSnapshot.ArtifactIds);
    }

    [Fact]
    public async Task S6_PreservedBytesCannotBeMutatedThroughNormalRecoveryUse()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        var expected = "immutable original"u8.ToArray();
        await File.WriteAllBytesAsync(Path.Combine(source, "original.bin"), expected, TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var session = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);
        var artifact = Assert.Single(session.State.Artifacts);
        var preservedPath = Path.Combine(workspace, artifact.PreservedByteReference!.Replace('/', Path.DirectorySeparatorChar));

        Assert.Equal(expected, await session.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        Assert.True(File.GetAttributes(preservedPath).HasFlag(FileAttributes.ReadOnly));
        Assert.Throws<UnauthorizedAccessException>(() => File.Open(preservedPath, FileMode.Open, FileAccess.Write, FileShare.None));
        Assert.Equal(expected, await session.ReadPreservedArtifactAsync(artifact.Id, TestContext.Current.CancellationToken));
        Assert.Equal(artifact.ContentHash!.Value.Value, Convert.ToHexStringLower(SHA256.HashData(await File.ReadAllBytesAsync(preservedPath, TestContext.Current.CancellationToken))));
    }

    [Fact]
    public async Task S7_ProcessRestartDoesNotCreateNewIdentitiesOrDuplicateState()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("supplied");
        await File.WriteAllTextAsync(Path.Combine(source, "one.bin"), "one", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "two.bin"), "two", TestContext.Current.CancellationToken);
        var workspace = Path.Combine(test.Root, "workspace");
        var original = await new LocalRecoveryWorkspaceApplication(workspace)
            .CreateAndIntakeAsync(new IntakeRequest(source), TestContext.Current.CancellationToken);

        var reopened = await new LocalRecoveryWorkspaceApplication(workspace).OpenAsync(TestContext.Current.CancellationToken);

        Assert.Equal(original.State.WorkspaceId, reopened.State.WorkspaceId);
        Assert.Equal(original.State.Run, reopened.State.Run);
        Assert.Equal(original.State.IntakeSnapshot!.Id, reopened.State.IntakeSnapshot!.Id);
        Assert.Equal(original.State.IntakeSnapshot.RecoveryRunId, reopened.State.IntakeSnapshot.RecoveryRunId);
        Assert.Equal(original.State.IntakeSnapshot.ArtifactIds, reopened.State.IntakeSnapshot.ArtifactIds);
        Assert.Equal(original.State.Checkpoint, reopened.State.Checkpoint);
        Assert.Equal(original.State.Artifacts, reopened.State.Artifacts);
        Assert.Equal(2, reopened.State.Artifacts.Select(item => item.Id).Distinct().Count());
    }

    private sealed class DeleteAfterEnumerationEnumerator(ISuppliedFileEnumerator inner, string failingRelativePath) : ISuppliedFileEnumerator
    {
        public async Task<SuppliedFileDiscoveryResult> EnumerateAsync(string suppliedDirectory, CancellationToken cancellationToken = default)
        {
            var discovery = await inner.EnumerateAsync(suppliedDirectory, cancellationToken);
            var failingFile = discovery.Files.Single(file => file.RelativePath == failingRelativePath);
            File.Delete(failingFile.SourcePath);
            return discovery;
        }
    }
}
