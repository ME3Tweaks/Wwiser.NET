using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class AuxParamsTests
{
    [TestCase(0x00)]
    [TestCase(0x03)]
    public void AuxParams_V134_Reserializes(byte value)
    {
        var byteData = new [] { value };
        var (_, result) = TestHelpers.Deserialize<AuxParams>(byteData, 134);

        var reserialized = TestHelpers.Serialize(result, 134);
        Assert.That(reserialized, Is.EquivalentTo(byteData));
    }

}