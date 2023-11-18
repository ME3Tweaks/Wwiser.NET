namespace ME3Tweaks.Wwiser.Tests;

public static class TestData
{
    public static Stream GetTestDataStream(string folder, string filename)
    {
        var file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData", folder, filename);
        return new FileStream(file, FileMode.Open);
    }
    
    public static Stream GetTestDataStream(string filename)
    {
        return GetTestDataStream("", filename);
    }

    public static byte[] GetTestDataBytes(string folder, string filename)
    {
        var file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData", folder, filename);
        return File.ReadAllBytes(file);
    }
    
    public static byte[] GetTestDataBytes(string filename)
    {
        return GetTestDataBytes("", filename);
    }
}