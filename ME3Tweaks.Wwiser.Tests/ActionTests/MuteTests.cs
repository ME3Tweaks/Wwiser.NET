using Action = ME3Tweaks.Wwiser.Model.Hierarchy.Action;

namespace ME3Tweaks.Wwiser.Tests.ActionTests;

public class MuteTests
{
    [TestCase("Mute_V56.bin", 56)]
    public void Mute_Reserializes(string file, int version)
    {
        var data = TestData.GetTestDataBytes(@"Action",file);
        var (_, result) = TestHelpers.Deserialize<Action>(data, version);
        
        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}