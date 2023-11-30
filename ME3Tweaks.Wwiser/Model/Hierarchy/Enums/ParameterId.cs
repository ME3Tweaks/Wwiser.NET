using System;
using System.IO;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class ParameterId : IBinarySerializable
{
    public RtpcParameterId? ParamId { get; set; }
    public ModulatorRtpcParameterId? ModParamId { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        switch (context.Version)
        {
            case <= 89:
                stream.Write(BitConverter.GetBytes((uint)ParamId!));
                break;
            case <= 113:
                stream.WriteByte((byte)ParamId!);
                break;
            default:
            {
                if (context.UseModulator)
                {
                    VarCount.WriteResizingUint(stream, (uint)ModParamId!);
                }
                else
                {
                    VarCount.WriteResizingUint(stream, (uint)ParamId!);
                }
                break;
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        switch (context.Version)
        {
            case <= 89:
                Span<byte> span = stackalloc byte[4];
                var read = stream.Read(span);
                if (read != 4) throw new Exception();
                ParamId = (RtpcParameterId)BitConverter.ToUInt32(span);
                break;
            case <= 113:
                ParamId = (RtpcParameterId)stream.ReadByte();
                break;
            default:
            {
                if (context.UseModulator)
                {
                    ModParamId = (ModulatorRtpcParameterId)VarCount.ReadResizingUint(stream);
                }
                else
                {
                    ParamId = (RtpcParameterId)VarCount.ReadResizingUint(stream);
                }
                break;
            }
        }
    }

    public enum RtpcParameterId : byte
    {
        Volume,
        LFE,
        Pitch,
        LPF,
        HPF,
        BusVolume,
        InitialDelay,
        MakeUpGain,
        FeedbackVolume,
        FeedbackLowpass,
        FeedbackPitch,
        MidiTransposition,
        MidiVelocityOffset,
        PlaybackSpeed,
        MuteRation,
        PlayMechanismSpecialTransitionsValue,
        MaxNumInstances,
        // Overridable params
        Priority,
        Position_PAN_X_2D,
        Position_PAN_Y_2D,
        Position_PAN_X_3D,
        Position_PAN_Y_3D,
        Position_Pan_Z_3D,
        PositioningTypeBlend,
        Positioning_Divergence_Center_PCT,
        Positioning_Cone_Attenuation_ON_OFF,
        Positioning_Cone_Attenuation,
        Positioning_Cone_LPF,
        Positioning_Cone_HPF,
        BypassFX0,
        BypassFX1,
        BypassFX2,
        BypassFX3,
        BypassAllFX,
        HDRBusThreshold,
        HDRBusReleaseTime,
        HDRBusRatio,
        HDRActiveRange,
        GameAuxSendVolume,
        UserAuxSendVolume0,
        UserAuxSendVolume1,
        UserAuxSendVolume2,
        UserAuxSendVolume3,
        OutputBusVolume,
        OutputBusLPF,
        OutputBusHPF,
        Positioning_EnableAttenuation,
        ReflectionsVolume,
        UserAuxSendLPF0,
        UserAuxSendLPF1,
        UserAuxSendLPF2,
        UserAuxSendLPF3,
        UserAuxSendHPF0,
        UserAuxSendHPF1,
        UserAuxSendHPF2,
        UserAuxSendHPF3,
        GameAuxSendLPF,
        GameAuxSendHPF,
        Position_PAN_Z_2D,
        BypassAllMetadata,
        UnknownCustom1 = 0x3D,
        UnknownCustom2 = 0x3E,
        UnknownCustom3 = 0x3F,
    }

    public enum ModulatorRtpcParameterId : byte
    {
        ModulatorLfoDepth,
        ModulatorLfoAttack,
        ModulatorLfoFrequency,
        ModulatorLfoWaveform,
        ModulatorLfoSmoothing,
        ModulatorLfoPWM,
        ModulatorLfoInitialPhase,
        ModulatorLfoRetrigger,
        ModulatorEnvelopeAttackTime,
        ModulatorEnvelopeAttackCurve,
        ModulatorEnvelopeDecayTime,
        ModulatorEnvelopeSustainLevel,
        ModulatorEnvelopeSustainTime,
        ModulatorEnvelopeReleaseTime,
        ModulatorTimePlaybackSpeed,
        ModulatorTimeInitialDelay
    }
}