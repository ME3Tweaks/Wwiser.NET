namespace ME3Tweaks.Wwiser.Tests;

public static class TestData
{
    public static Stream GetTestDataStream(string filename)
    {
        var file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData", filename);
        return new FileStream(file, FileMode.Open);
    }

    public static byte[] GetTestDataBytes(string filename)
    {
        var file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData", filename);
        return File.ReadAllBytes(file);
    }
}