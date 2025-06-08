using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class ParameterId : IBinarySerializable
{
    private RtpcParameterId? _paramId;
    private ModulatorRtpcParameterId? _modParamId;

    [Ignore]
    public RtpcParameterId? ParamId
    {
        get => _paramId;
        set
        {
            if(value is not null) _modParamId = null;
            _paramId = value;
        }
    }

    [Ignore]
    public ModulatorRtpcParameterId? ModParamId
    {
        get => _modParamId;
        set
        {
            if(value is not null) _paramId = null;
            _modParamId = value;
        }
    }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var value = GetSerializableValue(context.Version, context.UseModulator);
        
        switch (context.Version)
        {
            case <= 89:
                stream.Write(BitConverter.GetBytes((uint)value));
                break;
            case <= 113:
                stream.WriteByte(value);
                break;
            default:
            {
                if (context.UseModulator)
                {
                    VarCount.WriteResizingUint(stream, (uint)ModParamId!);
                }
                else
                {
                    VarCount.WriteResizingUint(stream, value);
                }
                break;
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var res = DeserializeStatic(stream, context.Version, context.UseModulator);
        (ParamId, ModParamId) = res;
    }

    public bool IsValidOnVersion(uint version)
    {
        try
        {
            GetSerializableValue(version, ModParamId.HasValue);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public static (RtpcParameterId?, ModulatorRtpcParameterId?) DeserializeStatic(Stream stream, uint version, bool useModulator)
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
            <= 65 => (ParameterIdVersionMap.V65.Value.Forward[value], null),
            72 => (ParameterIdVersionMap.V72.Value.Forward[value], null),
            88 => (ParameterIdVersionMap.V88.Value.Forward[value], null),
            >= 112 and <= 113 => ConvertV112ToEnum(value),
            >= 118 when useModulator => (null, (ModulatorRtpcParameterId)value)!,
            >= 118 and < 120 => (ConvertV118ToEnum(value), null),
            >= 120 and < 128 => (ConvertV120ToEnum(value), null),
            >= 128 and <= 134 => (ConvertV128ToEnum(value), null),
            >= 135 when value <= 0x3F => ((RtpcParameterId)value, null),
            _ => throw new ArgumentException($"No known parameter id mapping for version {version}", nameof(version))
        };
    }
    
    private byte GetSerializableValue(uint version, bool useModulator)
    {
        var paramId = ParamId ?? 0;
        return version switch
        {
            <= 65 => ParameterIdVersionMap.V65.Value.Reverse[paramId],
            72 => ParameterIdVersionMap.V72.Value.Reverse[paramId],
            88 => ParameterIdVersionMap.V88.Value.Reverse[paramId],
            >= 112 and <= 113 => ConvertV112ToByte(ParamId, ModParamId),
            >= 118 when ModParamId.HasValue && useModulator => (byte)ModParamId.Value,
            >= 118 and < 120 => ConvertV118ToByte(paramId),
            >= 120 and < 128 => ConvertV120ToByte(paramId),
            >= 128 and <= 134 => ConvertV128ToByte(paramId),
            >= 135 when paramId <= RtpcParameterId.UnknownCustom3 => (byte)paramId,
            _ => throw new ArgumentException($"No known parameter id mapping for version {version}", nameof(version))
        };
    }

    private static (RtpcParameterId?, ModulatorRtpcParameterId?) ConvertV112ToEnum(byte id)
    {
        if (ParameterIdVersionMap.V112.Value.Forward.TryGetValue(id, out var value))
        {
            return (value, null);
        }
        if (id is >= 0x2A and <= 0x37)
        {
            return (null, (ModulatorRtpcParameterId)(id - 0x2A));
        }
        throw new ArgumentException($"ID is out of range for v112", nameof(id));
    }
    
    private static byte ConvertV112ToByte(RtpcParameterId? rtpc, ModulatorRtpcParameterId? mod)
    {
        if (rtpc.HasValue && ParameterIdVersionMap.V112.Value.Reverse.TryGetValue(rtpc.Value, out var id))
        {
            return id;
        }
        else if (mod is <= ModulatorRtpcParameterId.ModulatorEnvelopeReleaseTime)
        {
            return (byte)((byte)mod.Value + 0x2A);
        }
        throw new ArgumentException($"Parameter id is not valid for v112 - v113");
    }

    private static RtpcParameterId ConvertV118ToEnum(byte id)
    {
        if(id > 0x2D) throw new ArgumentException("ID is out of range for v118", nameof(id));

        if (ParameterIdVersionMap.V118.Value.Forward.TryGetValue(id, out var value))
        {
            return value;
        }
        return (RtpcParameterId)id;
    }
    
    private static byte ConvertV118ToByte(RtpcParameterId id)
    {
        if(id > RtpcParameterId.OutputBusLPF)
        {
            throw new ArgumentException($"{id} is not valid for v118", nameof(id));
        }
        
        if (ParameterIdVersionMap.V118.Value.Reverse.TryGetValue(id, out var value))
        {
            return value;
        }
        return (byte)id;
    }
    
    private static RtpcParameterId ConvertV120ToEnum(byte id)
    {
        if(id > 0x2E) throw new ArgumentException("ID is out of range for v120 - v127", nameof(id));
        return (RtpcParameterId)id;
    }
    
    private static byte ConvertV120ToByte(RtpcParameterId id)
    {
        if(id > RtpcParameterId.Positioning_EnableAttenuation)
        {
            throw new ArgumentException($"{id} is not valid for v120 - v127", nameof(id));
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

        if (ParameterIdVersionMap.V128.Value.Forward.TryGetValue(id, out var value))
        {
            return value;
        }
        return (RtpcParameterId)id;
    }
    
    private static byte ConvertV128ToByte(RtpcParameterId id)
    {
        if(id is RtpcParameterId.ReflectionsVolume or RtpcParameterId.Position_PAN_Z_2D or RtpcParameterId.BypassAllMetadata or RtpcParameterId.MaxNumRTPC)
        {
            throw new ArgumentException($"{id} is not valid for v120 - v134", nameof(id));
        }
        
        if (ParameterIdVersionMap.V128.Value.Reverse.TryGetValue(id, out var value))
        {
            return value;
        }
        return (byte)id;
    }

    // ReSharper disable InconsistentNaming
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
        // These are custom for different games - not relevant for Mass Effect. Probably should never be converting versions that use these.
        UnknownCustom1 = 0x3D,
        UnknownCustom2 = 0x3E,
        UnknownCustom3 = 0x3F,
        UnknownCustom4 = 0x40,
        UnknownCustom5 = 0x41,
        UnknownCustom6 = 0x42,
        
        Positioning_Radius_LPF = 0x50, // v72
        Positioning_Radius_SIM_ON_OFF = 0x51, // v56<=
        Positioning_Radius_SIM_Attenuation = 0x52, // v56<=
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
    // ReSharper restore InconsistentNaming
}