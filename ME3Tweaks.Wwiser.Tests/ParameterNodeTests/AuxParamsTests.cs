using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class AuxParamsTests
{
    [TestCase(0x00)]
    [TestCase(0x03)]
    public void AuxParams_V134_Reserializes(byte value)
    {
        var byteData = new byte[] { value };
        var version = 134;
        var (_, result) = TestHelpers.Deserialize<AuxParams>(byteData, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(byteData));
    }

}