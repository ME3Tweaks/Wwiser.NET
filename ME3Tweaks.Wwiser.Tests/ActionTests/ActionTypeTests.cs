using ME3Tweaks.Wwiser.Model.Action;

namespace ME3Tweaks.Wwiser.Tests.ActionTests;

public class ActionTypeTests
{
    [TestCase(0x01011, ActionTypeValue.Stop)]
    [TestCase(0x09011, ActionTypeValue.SetPitch2)]
    [TestCase(0x0A011, ActionTypeValue.SetBusVolume1)]
    [TestCase(0x0C011, ActionTypeValue.SetLFE1)]
    [TestCase(0x0E011, ActionTypeValue.SetLPF1)]
    public void ActionTypeParsesAndReserializes_V56(int hex, ActionTypeValue expected)
    {
        var bytes = BitConverter.GetBytes((uint)hex);
        var (_, result) = TestHelpers.Deserialize<ActionType>(bytes, 56);
        Assert.That(result.Value, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 56);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
    
    [TestCase(0x0101, ActionTypeValue.Stop)]
    [TestCase(0x0901, ActionTypeValue.SetPitch2)]
    [TestCase(0x0A01, ActionTypeValue.SetNone1)]
    [TestCase(0x0B01, ActionTypeValue.SetNone2)]
    [TestCase(0x0C01, ActionTypeValue.SetBusVolume1)]
    [TestCase(0x0E01, ActionTypeValue.SetLPF1)]
    [TestCase(0x1901, ActionTypeValue.SetSwitch)]
    [TestCase(0x1A01, ActionTypeValue.BypassFX1)]
    public void ActionTypeParsesAndReserializes_V72(int hex, ActionTypeValue expected)
    {
        var bytes = BitConverter.GetBytes((ushort)hex);
        
        var (_, result) = TestHelpers.Deserialize<ActionType>(bytes, 72);
        Assert.That(result.Value, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 72);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
}