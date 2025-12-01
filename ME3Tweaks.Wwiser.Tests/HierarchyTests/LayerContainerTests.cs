using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class LayerContainerTests
{
    [TestCase("Layer_v134.bin", 134)]
    public void LayerContainer_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"LayerContainer", filename);
        var (_, result) = TestHelpers.Deserialize<LayerContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}