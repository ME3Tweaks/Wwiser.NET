using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class RandSeqContainerTests
{
    
    [TestCase("RandSeqContainer_V56.bin", 56)]
    public void RandSeqContainer_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"RandSeqContainer", filename);
        var (_, result) = TestHelpers.Deserialize<RandSeqContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}