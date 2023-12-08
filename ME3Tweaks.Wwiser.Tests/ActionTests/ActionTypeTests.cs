using ME3Tweaks.Wwiser.Model.Action;

namespace ME3Tweaks.Wwiser.Tests.ActionTests;

public class ActionTypeTests
{
    [TestCase(0x01, ActionTypeValue.Stop)]
    [TestCase(0x09, ActionTypeValue.SetPitch2)]
    [TestCase(0x0A, ActionTypeValue.SetBusVolume1)]
    [TestCase(0x0C, ActionTypeValue.SetLFE1)]
    [TestCase(0x0E, ActionTypeValue.SetLPF1)]
    public void ActionTypeParsesAndReserializes_V56(int hex, ActionTypeValue expected)
    {
        var (_, result) = TestHelpers.Deserialize<ActionType>((byte)hex, 56);
        Assert.That(result.Value, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 56);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
    
    [TestCase(0x01, ActionTypeValue.Stop)]
    [TestCase(0x09, ActionTypeValue.SetPitch2)]
    [TestCase(0x0A, ActionTypeValue.SetNone1)]
    [TestCase(0x0B, ActionTypeValue.SetNone2)]
    [TestCase(0x0C, ActionTypeValue.SetBusVolume1)]
    [TestCase(0x0E, ActionTypeValue.SetLPF1)]
    [TestCase(0x19, ActionTypeValue.SetSwitch)]
    [TestCase(0x1A, ActionTypeValue.BypassFX1)]
    public void ActionTypeParsesAndReserializes_V72(byte hex, ActionTypeValue expected)
    {
        var (_, result) = TestHelpers.Deserialize<ActionType>(hex, 72);
        Assert.That(result.Value, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 72);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
}