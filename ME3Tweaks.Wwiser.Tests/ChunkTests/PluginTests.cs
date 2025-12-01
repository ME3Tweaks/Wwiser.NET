namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class PluginTests
{
    [Test]
    public void INIT_V134_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"PluginChunk",@"INIT_v134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        var reserialized = TestHelpers.Serialize(result, 134);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}