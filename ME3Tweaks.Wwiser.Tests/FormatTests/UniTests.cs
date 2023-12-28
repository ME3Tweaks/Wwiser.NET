using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Tests.FormatTests;

public class UniTests
{
    [TestCase(new byte[] {0x0, 0x0, 0x0, 0x0 })]
    [TestCase(new byte[] {0x0, 0x0, 0xC0, 0xC0 })]
    [TestCase(new byte[] {0x0, 0x0, 0x40, 0xC1 })]
    [TestCase(new byte[] {0xBF, 0x65, 0xD7, 0x17 })]
    public void UniReserializes(byte[] data)
    {
        var (_, result) = TestHelpers.Deserialize<Uni>(data, 113);
        var reserialized = TestHelpers.Serialize(result, 113);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}