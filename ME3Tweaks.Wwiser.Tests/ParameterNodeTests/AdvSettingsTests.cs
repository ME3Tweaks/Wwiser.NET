using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class AdvSettingsTests
{
    [TestCase("Empty_V134.bin", 134)]
    [TestCase("Empty_V44.bin", 44)]
    public void Empty_MultipleVersions_Reserializes(string file, int version)
    {
        var data = TestData.GetTestDataBytes(@"ParameterNode",@"AdvSettings", file);
        var (_, result) = TestHelpers.Deserialize<AdvSettingsParams>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}