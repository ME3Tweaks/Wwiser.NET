using System.Runtime.InteropServices;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

//TODO: Parse this properly. This will suck a lot.
public class ParameterId : IBinarySerializable
{
    [Ignore]
    public RtpcParameterId? ParamId { get; set; }
    
    [Ignore]
    public ModulatorRtpcParameterId? ModParamId { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var paramId = GetSerializableValue(context.Version);
        
        switch (context.Version)
        {
            case <= 89:
                stream.Write(BitConverter.GetBytes((uint)paramId));
                break;
            case <= 113:
                stream.WriteByte(paramId);
                break;
            default:
            {
                if (context.UseModulator)
                {
                    VarCount.WriteResizingUint(stream, (uint)ModParamId!);
                }
                else
                {
                    VarCount.WriteResizingUint(stream, paramId);
                }
                break;
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        
        if(context.Version > 113 && context.UseModulator)
        {
            ModParamId = DeserializeStaticModulator(stream, context.Version);
            return;
        }
        
        ParamId = DeserializeStatic(stream, context.Version);
    }

    public bool IsValidOnVersion(uint version)
    {
        try
        {
            GetSerializableValue(version);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public static RtpcParameterId DeserializeStatic(Stream stream, uint version)
    {
        byte value;
        switch (version)
        {
            case <= 89:
                Span<byte> span = stackalloc byte[4];
                var read = stream.Read(span);
                if (read != 4) throw new Exception();
                value = (byte)BitConverter.ToUInt32(span);
                break;
            case <= 113:
                value = (byte)stream.ReadByte();
                break;
            default:
                value = (byte)VarCount.ReadResizingUint(stream);
                break;
        }

        return version switch
        {
            >= 120 and < 128 => ConvertV120ToEnum(value),
            >= 128 and <= 134 => ConvertV128ToEnum(value),
            _ => (RtpcParameterId)value
        };
    }
    
    public static ModulatorRtpcParameterId DeserializeStaticModulator(Stream stream, uint version)
    {
        return (ModulatorRtpcParameterId)VarCount.ReadResizingUint(stream);
    }
    
    private byte GetSerializableValue(uint version)
    {
        return version switch
        {
            >= 120 and < 128 => ConvertV120ToByte(ParamId ?? 0),
            >= 128 and <= 134 => ConvertV128ToByte(ParamId ?? 0),
            _ => (byte)(ParamId ?? 0)
        };
    }

    private static byte ConvertV120ToByte(RtpcParameterId id)
    {
        if(id > RtpcParameterId.Positioning_EnableAttenuation)
        {
            throw new ArgumentException($"{id} is not valid for v120 - v127", nameof(id));
        }
        return (byte)id;
    }
    
    private static RtpcParameterId ConvertV120ToEnum(byte id)
    {
        if(id > 0x2E) throw new ArgumentException("ID is out of range for v120 - v127", nameof(id));
        return (RtpcParameterId)id;
    }

    private static byte ConvertV128ToByte(RtpcParameterId id)
    {
        if(id is RtpcParameterId.ReflectionsVolume or RtpcParameterId.Position_PAN_Z_2D or RtpcParameterId.BypassAllMetadata or RtpcParameterId.MaxNumRTPC)
        {
            throw new ArgumentException($"{id} is not valid for v120 - v134", nameof(id));
        }
        
        if (ParameterIdMap.V128.Value.Reverse.TryGetValue(id, out var value))
        {
            return value;
        }
        return (byte)id;
    }

    private static RtpcParameterId ConvertV128ToEnum(byte id)
    {
        switch (id)
        {
            case > 0x41:
                throw new ArgumentException("ID is out of range for v128 - v134", nameof(id));
            case > 0x38 and < 0x3C:
                throw new ArgumentException("Unknown custom ID for v128 - v134", nameof(id));
        }

        if (ParameterIdMap.V128.Value.Forward.TryGetValue(id, out var value))
        {
            return value;
        }
        return (RtpcParameterId)id;
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
        FeedbackVolume, // Deprecated on >= v128
        FeedbackLowpass, // Deprecated on >= v128
        FeedbackPitch, // Deprecated on >= v128
        MidiTransposition,
        MidiVelocityOffset,
        PlaybackSpeed,
        MuteRatio,
        PlayMechanismSpecialTransitionsValue,
        MaxNumInstances,
        // Overridable params
        Priority,
        Position_PAN_X_2D,
        Position_PAN_Y_2D,
        Position_PAN_X_3D,
        Position_PAN_Y_3D,
        Position_Pan_Z_3D,
        PositioningTypeBlend, // PositioningType < 132
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
        Positioning_EnableAttenuation, // Attenuation < 134
        ReflectionsVolume,
        // >= 128
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
        MaxNumRTPC = 0x3C,
        UnknownCustom1 = 0x3D,
        UnknownCustom2 = 0x3E,
        UnknownCustom3 = 0x3F, // stops here on 135
        UnknownCustom4 = 0x40,
        UnknownCustom5 = 0x41,
        UnknownCustom6 = 0x42,
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