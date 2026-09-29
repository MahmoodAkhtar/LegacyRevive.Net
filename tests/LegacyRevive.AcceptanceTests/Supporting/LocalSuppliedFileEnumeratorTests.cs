using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local.Artifacts;

namespace LegacyRevive.AcceptanceTests.Supporting;

public sealed class LocalSuppliedFileEnumeratorTests
{
    [Fact]
    public async Task EmptyDirectoryIsCompleteAndKnownEmpty()
    {
        using var test = new TestWorkspace();
        var result = await new LocalSuppliedFileEnumerator()
            .EnumerateAsync(test.CreateDirectory("source"), TestContext.Current.CancellationToken);

        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Complete, result.Completeness);
        Assert.Empty(result.Files);
        Assert.Empty(result.Diagnostics);
    }

    [Fact]
    public async Task RelativePathsAreNormalizedAndDeterministicallyOrdered()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("source");
        Directory.CreateDirectory(Path.Combine(source, "nested"));
        await File.WriteAllTextAsync(Path.Combine(source, "z.bin"), "z", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "nested", "a.bin"), "a", TestContext.Current.CancellationToken);

        var result = await new LocalSuppliedFileEnumerator().EnumerateAsync(source, TestContext.Current.CancellationToken);

        Assert.Equal(["nested/a.bin", "z.bin"], result.Files.Select(file => file.RelativePath));
        Assert.All(result.Files, file => Assert.DoesNotContain('\\', file.RelativePath));
    }

    [Fact]
    public async Task RootFailureIsCapturedWithoutFilesOrSyntheticPaths()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("source");
        var result = await new FailingEnumerator(source)
            .EnumerateAsync(source, TestContext.Current.CancellationToken);

        Assert.True(result.IsRootBlocked);
        Assert.Empty(result.Files);
        var diagnostic = Assert.Single(result.Diagnostics);
        Assert.Equal(IntakeDiscoveryScopeKind.SuppliedSource, diagnostic.Scope.Kind);
        Assert.Equal(".", diagnostic.Scope.RelativePath);
    }

    [Fact]
    public async Task SubtreeFailureRetainsSafeSiblingAndDoesNotInventChildren()
    {
        using var test = new TestWorkspace();
        var source = test.CreateDirectory("source");
        var blocked = Path.Combine(source, "blocked");
        Directory.CreateDirectory(blocked);
        await File.WriteAllTextAsync(Path.Combine(blocked, "unknown.bin"), "not discoverable", TestContext.Current.CancellationToken);
        await File.WriteAllTextAsync(Path.Combine(source, "safe.bin"), "safe", TestContext.Current.CancellationToken);

        var result = await new FailingEnumerator(blocked)
            .EnumerateAsync(source, TestContext.Current.CancellationToken);

        Assert.Equal(SuppliedSourceDiscoveryCompleteness.Incomplete, result.Completeness);
        Assert.Equal("safe.bin", Assert.Single(result.Files).RelativePath);
        var diagnostic = Assert.Single(result.Diagnostics);
        Assert.Equal(IntakeDiscoveryScopeKind.DirectorySubtree, diagnostic.Scope.Kind);
        Assert.Equal("blocked", diagnostic.Scope.RelativePath);
        Assert.DoesNotContain(result.Files, file => file.RelativePath == "blocked/unknown.bin");
    }

    [Fact]
    public async Task CancellationRemainsCancellation()
    {
        using var test = new TestWorkspace();
        var cancellation = new CancellationToken(canceled: true);

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            new LocalSuppliedFileEnumerator().EnumerateAsync(test.CreateDirectory("source"), cancellation));
    }

    private sealed class FailingEnumerator(string failingDirectory) : LocalSuppliedFileEnumerator
    {
        private readonly string _failingDirectory = Path.GetFullPath(failingDirectory);

        protected override IEnumerable<string> EnumerateFiles(string directoryPath)
        {
            if (string.Equals(Path.GetFullPath(directoryPath), _failingDirectory, StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("Injected directory enumeration failure.");
            }

            return base.EnumerateFiles(directoryPath);
        }
    }
}
