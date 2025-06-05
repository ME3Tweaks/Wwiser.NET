using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class SoundTests
{
    [TestCase("Sound_V134.bin", 134, 280385228u, (short)0)]
    [TestCase("Sound_V56.bin", 56, 131546226u,  (short)0)]
    public void Sound_ParsesCorrectly(string filename, int version, uint sourceId, short loop)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Sound", filename);
        var (_, result) = TestHelpers.Deserialize<Sound>(data, version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.BankSourceData.MediaInformation.SourceId, Is.EqualTo(sourceId));
            Assert.That(result.Loop, Is.EqualTo(loop));
        });
    }
    
    [TestCase("Sound_V134.bin", 134)]
    [TestCase("Sound_V56.bin", 56)]
    [TestCase("Sound_V44.bin", 44)]
    public void Sound_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Sound", filename);
        var (_, result) = TestHelpers.Deserialize<Sound>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}