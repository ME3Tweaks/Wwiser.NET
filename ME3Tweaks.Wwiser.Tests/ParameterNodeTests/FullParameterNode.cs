using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class FullParameterNode
{
    [TestCase("A_V134.bin", 134)]
    [TestCase("A_V56.bin", 56)]
    public void FullParameterNode_MultipleVersions_Reserializes(string file, int version)
    {
        var data = TestData.GetTestDataBytes(@"ParameterNode", file);
        var (_, result) = TestHelpers.Deserialize<NodeBaseParameters>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}