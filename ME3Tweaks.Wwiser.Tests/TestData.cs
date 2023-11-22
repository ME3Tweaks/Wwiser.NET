namespace ME3Tweaks.Wwiser.Tests;

public static class TestData
{
    private static readonly string[] TestDir = { TestContext.CurrentContext.WorkDirectory, "TestData" };
    public static Stream GetTestDataStream(params string[] args)
    {
        var file = Path.Combine(TestDir.Concat(args).ToArray());
        if (!File.Exists(file))
        {
            throw new FileNotFoundException();
        }
        
        return File.OpenRead(file);
    }

    public static byte[] GetTestDataBytes(params string[] args)
    {
        var file = Path.Combine(TestDir.Concat(args).ToArray());
        if (!File.Exists(file))
        {
            throw new FileNotFoundException();
        }
        
        return File.ReadAllBytes(file);
    }
    
    public static string GetTestDataFilePath(params string[] args)
    {
        return Path.Combine(TestDir.Concat(args).ToArray());
    }
}