namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class EnvironmentSettingsTests
{
    [TestCase("ENVS_v134.bin", 134)]
    [TestCase("ENVS_v56.bin", 56)]
    public void ENVS_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"EnvironmentSettings", filename);
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}