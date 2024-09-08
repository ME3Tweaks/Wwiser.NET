using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class SwitchContainerTests
{
    [TestCase("SwitchContainer_V56.bin", 56)]
    public void SwitchContainer_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"SwitchContainer", filename);
        var (_, result) = TestHelpers.Deserialize<SwitchContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}