using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.FormatTests;

public class SmartPropIdTests
{
    [TestCase(0x00, PropId.Volume)]
    [TestCase(0x03, PropId.LPF)]
    [TestCase(0x04, PropId.Priority)]
    [TestCase(0x0F, PropId.Probability)]
    [TestCase(0x10, PropId.Probability, true)]
    [TestCase(0x11, PropId.DialogueMode)]
    [TestCase(0x17, PropId.OutputBusVolume)]
    [TestCase(0x18, PropId.OutputBusLPF)]
    public void PropsIdParsesAndReserializes_V65(int hex, PropId expected, bool noReserialize = false)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>((byte)hex, 65);
        Assert.That(result.PropValue, Is.EqualTo(expected));

        if (!noReserialize)
        {
            var reserialized = TestHelpers.Serialize(result, 65);
            Assert.That(reserialized[0], Is.EqualTo(hex));
        }
    }
    
    [TestCase(0x00, PropId.Volume)]
    [TestCase(0x03, PropId.LPF)]
    [TestCase(0x04, PropId.BusVolume)]
    //[TestCase(0x36, PropId.PlaybackSpeed)]
    [TestCase(0x17, PropId.OutputBusVolume)]
    [TestCase(0x18, PropId.OutputBusLPF)]
    [TestCase(0x2C, PropId.CrossfadeDownCurve)]
    public void PropsIdParsesAndReserializes_V88(byte hex, PropId expected)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>(hex, 88);
        Assert.That(result.PropValue, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 88);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
    
    [TestCase(0x00, PropId.Volume)]
    [TestCase(0x07, PropId.PriorityDistanceOffset)]
    [TestCase(0x08, PropId.FeedbackVolume)]
    [TestCase(0x2C, PropId.CrossfadeDownCurve)]
    [TestCase(0x2D, PropId.MidiTrackingRootNote)]
    [TestCase(0x3A, PropId.Loop)]
    [TestCase(0x3B, PropId.InitialDelay)]
    public void PropsIdParsesAndReserializes_V113(byte hex, PropId expected)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>(hex, 113);
        Assert.That(result.PropValue, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 113);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
    
    [TestCase(0x00, PropId.Volume)]
    [TestCase(0x05, PropId.BusVolume)]
    [TestCase(0x06, PropId.MakeUpGain)]
    [TestCase(0x07, PropId.Priority)]
    [TestCase(0x09, PropId.FeedbackVolume)]
    [TestCase(0x1A, PropId.OutputBusLPF)]
    [TestCase(0x1B, PropId.HDRBusThreshold)]
    [TestCase(0x2D, PropId.MidiTrackingRootNote)]
    [TestCase(0x21, PropId.HDRActiveRange)]
    [TestCase(0x22, PropId.LoopStart)]
    [TestCase(0x3A, PropId.Loop)]
    [TestCase(0x3B, PropId.InitialDelay)]
    [TestCase(0x39, PropId.AttachedPluginFXID)]
    [TestCase(0x3C, PropId.UserAuxSendLPF0)]
    public void PropsIdParsesAndReserializes_V128(byte hex, PropId expected)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>(hex, 128);
        Assert.That(result.PropValue, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 128);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
    
    [TestCase(0x00, ModulatorPropId.Scope)]
    [TestCase(0x08, ModulatorPropId.Lfo_InitialPhase)]
    [TestCase(0x09, ModulatorPropId.Envelope_AttackTime)]
    [TestCase(0x0A, ModulatorPropId.Envelope_AttackCurve)]
    [TestCase(0x13, ModulatorPropId.Time_InitialDelay)]
    public void ModulatorPropsIdParsesAndReserializes_V125(byte hex, ModulatorPropId expected)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>(hex, 125, true);
        Assert.That(result.ModulatorValue, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 125, true);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
    
    [TestCase(0x00, ModulatorPropId.Scope)]
    [TestCase(0x08, ModulatorPropId.Lfo_InitialPhase)]
    [TestCase(0x09, ModulatorPropId.Lfo_Retrigger)]
    [TestCase(0x0A, ModulatorPropId.Envelope_AttackTime)]
    [TestCase(0x14, ModulatorPropId.Time_InitialDelay)]
    public void ModulatorPropsIdParsesAndReserializes_V150(byte hex, ModulatorPropId expected)
    {
        var (_, result) = TestHelpers.Deserialize<SmartPropId>(hex, 150, true);
        Assert.That(result.ModulatorValue, Is.EqualTo(expected));
        
        var reserialized = TestHelpers.Serialize(result, 150, true);
        Assert.That(reserialized[0], Is.EqualTo(hex));
    }
}