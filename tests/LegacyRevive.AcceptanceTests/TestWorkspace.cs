namespace LegacyRevive.AcceptanceTests;

internal sealed class TestWorkspace : IDisposable
{
    public TestWorkspace()
    {
        Root = Path.Combine(Path.GetTempPath(), "LegacyRevive.Tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Root);
    }

    public string Root { get; }
    public string CreateDirectory(string name)
    {
        var path = Path.Combine(Root, name);
        Directory.CreateDirectory(path);
        return path;
    }

    public void Dispose()
    {
        if (!Directory.Exists(Root))
        {
            return;
        }

        foreach (var file in Directory.EnumerateFiles(Root, "*", SearchOption.AllDirectories))
        {
            File.SetAttributes(file, FileAttributes.Normal);
        }

        Directory.Delete(Root, recursive: true);
    }
}
