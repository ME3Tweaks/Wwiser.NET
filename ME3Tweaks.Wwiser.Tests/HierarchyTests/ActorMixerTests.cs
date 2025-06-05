using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class ActorMixerTests
{
    [TestCase("ActorMixer_V134.bin", 134, 533330722)]
    [TestCase("ActorMixer_V56.bin", 56, 235120940)]
    public void ActorMixer_ParsesCorrectly(string filename, int version, int firstChild)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"ActorMixer", filename);
        var (_, result) = TestHelpers.Deserialize<ActorMixer>(data, (uint)version);
        Assert.That(result.Children[0], Is.EqualTo(firstChild));
    }
    
    [TestCase("ActorMixer_V134.bin", 134)]
    [TestCase("ActorMixer_V56.bin", 56)]
    public void ActorMixer_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"ActorMixer", filename);
        var (_, result) = TestHelpers.Deserialize<ActorMixer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}