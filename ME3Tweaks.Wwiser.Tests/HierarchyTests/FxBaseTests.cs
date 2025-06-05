using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class FxBaseTests
{
    [TestCase("FxCustomV134.bin", 134,  0x006E1003, 0x8B)]
    public void FxCustom_ParsesCorrectly(string filename, int version, int pluginId, int dataSize)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"FxCustom", filename);
        var (serializer, result) = TestHelpers.Deserialize<FxCustom>(data, version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Plugin.PluginId, Is.EqualTo(pluginId));
            Assert.That(result.PluginParameters.ParamSize, Is.EqualTo(dataSize));
        });
    }
}