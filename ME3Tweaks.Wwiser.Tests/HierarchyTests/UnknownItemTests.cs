using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class UnknownItemTests
{
    [Test]
    public void UnparsedHIRCItem_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"Bus", "Bus_v56.bin");
        var (_, result) = TestHelpers.Deserialize<HircItemContainer>(data, 56);
        
        Assert.That(result.Item, Is.TypeOf<EmptyHircItem>());

        var reserialized = TestHelpers.Serialize(result, 56);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}