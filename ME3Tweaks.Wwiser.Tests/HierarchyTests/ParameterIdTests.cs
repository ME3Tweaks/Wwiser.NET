using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using RtpcParameterId = ME3Tweaks.Wwiser.Model.Hierarchy.Enums.ParameterId.RtpcParameterId;
using ModulatorRtpcParameterId = ME3Tweaks.Wwiser.Model.Hierarchy.Enums.ParameterId.ModulatorRtpcParameterId;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class ParameterIdTest
{
    [TestCase(0x0,  RtpcParameterId.Volume)]
    [TestCase(0xA,  RtpcParameterId.Positioning_Radius_LPF)]
    [TestCase(0x1F,  RtpcParameterId.FeedbackPitch)]
    public void ParameterID_ParsesAndReserializes_V65(int hex, RtpcParameterId expected)
    {
        byte[] bytes = BitConverter.GetBytes((uint)hex);
    
        var (_, result) = TestHelpers.Deserialize<ParameterId>(bytes, 65);
        Assert.That(result.ParamId, Is.EqualTo(expected));
        Assert.That(result.ModParamId, Is.Null);
    
        var reserialized = TestHelpers.Serialize(result, 65);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
    
    [TestCase(0x2A,  ModulatorRtpcParameterId.ModulatorLfoDepth)]
    [TestCase(0x2F,  ModulatorRtpcParameterId.ModulatorLfoPWM)]
    [TestCase(0x37,  ModulatorRtpcParameterId.ModulatorEnvelopeReleaseTime)]
    public void ModulatorParameterID_ParsesAndReserializes_V112(int hex, ModulatorRtpcParameterId expected)
    {
        byte[] bytes = [(byte)hex];
    
        var (_, result) = TestHelpers.Deserialize<ParameterId>(bytes, 112);
        Assert.That(result.ModParamId, Is.EqualTo(expected));
        Assert.That(result.ParamId, Is.Null);
    
        var reserialized = TestHelpers.Serialize(result, 112);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
    
    [TestCase(0x0,  RtpcParameterId.Volume)]
    [TestCase(0xA,  RtpcParameterId.PositioningTypeBlend)]
    [TestCase(0x38,  RtpcParameterId.UserAuxSendVolume0)]
    [TestCase(0x29,  RtpcParameterId.PlaybackSpeed)]
    public void ParameterID_ParsesAndReserializes_V112(int hex, RtpcParameterId expected)
    {
        byte[] bytes = [(byte)hex];
    
        var (_, result) = TestHelpers.Deserialize<ParameterId>(bytes, 112);
        Assert.That(result.ParamId, Is.EqualTo(expected));
        Assert.That(result.ModParamId, Is.Null);
    
        var reserialized = TestHelpers.Serialize(result, 112);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }

    [TestCase(RtpcParameterId.UserAuxSendLPF0)]
    public void ParameterID_ThrowsErrorOnBadEnum_V120(RtpcParameterId id)
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TestHelpers.Serialize(new ParameterId { ParamId = id }, 120);
        });
    }
    
    [TestCase(0x0,  RtpcParameterId.Volume)]
    [TestCase(0x10,  RtpcParameterId.MaxNumInstances)]
    [TestCase(0x1D,  RtpcParameterId.BypassFX0)]
    [TestCase(0x2E,  RtpcParameterId.Positioning_EnableAttenuation)]
    [TestCase(0x2F,  RtpcParameterId.UserAuxSendLPF0)]
    [TestCase(0x38,  RtpcParameterId.GameAuxSendHPF)]
    [TestCase(0x3E,  RtpcParameterId.UnknownCustom3)]
    public void ParameterID_ParsesAndReserializes_V128(int hex, RtpcParameterId expected)
    {
        byte[] bytes = [(byte)hex];
    
        var (_, result) = TestHelpers.Deserialize<ParameterId>(bytes, 128);
        Assert.That(result.ParamId, Is.EqualTo(expected));
    
        var reserialized = TestHelpers.Serialize(result, 128);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
    
    /// <summary>
    /// Tests reserializing a ParameterId with a version that does not support the enum value.
    /// </summary>
    /// <param name="id"></param>
    [TestCase(RtpcParameterId.ReflectionsVolume)]
    [TestCase(RtpcParameterId.Position_PAN_Z_2D)]
    [TestCase(RtpcParameterId.BypassAllMetadata)]
    public void ParameterID_ThrowsErrorOnBadEnum_V128(RtpcParameterId id)
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TestHelpers.Serialize(new ParameterId { ParamId = id }, 128);
        });
    }
    
    /// <summary>
    /// Tests deserializing a ParameterId with a byte that does not map to a valid enum value in version 120.
    /// </summary>
    /// <param name="hex"></param>
    [TestCase(0x39)]
    [TestCase(0x3B)]
    [TestCase(0x42)]
    public void ParameterID_ThrowsErrorOnBadByte_V128(byte hex)
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            byte[] bytes = [hex];
            TestHelpers.Deserialize<ParameterId>(bytes, 128);
        });
    }

    [TestCase(0x0,  RtpcParameterId.Volume)]
    [TestCase(0x10,  RtpcParameterId.MaxNumInstances)]
    [TestCase(0x1D,  RtpcParameterId.BypassFX0)]
    [TestCase(0x3B,  RtpcParameterId.BypassAllMetadata)]
    [TestCase(0x3F,  RtpcParameterId.UnknownCustom3)]
    public void ParameterID_ParsesAndReserializes_V135(int hex, RtpcParameterId expected)
    {
        byte[] bytes = [(byte)hex];
    
        var (_, result) = TestHelpers.Deserialize<ParameterId>(bytes, 135);
        Assert.That(result.ParamId, Is.EqualTo(expected));
    
        var reserialized = TestHelpers.Serialize(result, 135);
        Assert.That(reserialized, Is.EquivalentTo(bytes));
    }
}