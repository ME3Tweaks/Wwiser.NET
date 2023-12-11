using Action = ME3Tweaks.Wwiser.Model.Hierarchy.Action;

namespace ME3Tweaks.Wwiser.Tests.ActionTests;

public class StopTests
{
    [TestCase("Stop_V134.bin", 134)]
    [TestCase("Stop_V56.bin", 56)]
    [TestCase("Stop_V44.bin", 44)]
    public void Stop_Reserializes(string file, int version)
    {
        var data = TestData.GetTestDataBytes(@"Action",file);
        var (_, result) = TestHelpers.Deserialize<Action>(data, version);
        
        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}