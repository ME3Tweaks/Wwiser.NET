using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class ParameterId : IBinarySerializable
{
    private RtpcParameterId? _paramId;
    private ModulatorRtpcParameterId? _modParamId;
    private byte? _customValue;

    [Ignore]
    public byte? CustomValue
    {
        get => _customValue;
        set
        {
            if (value is not null)
            {
                _paramId = RtpcParameterId.UnknownCustom1;
                _modParamId = null;
            }

            _customValue = value;
        }
    }

    [Ignore]
    public RtpcParameterId? ParamId
    {
        get => _paramId;
        set
        {
            if (value is not null && value != RtpcParameterId.UnknownCustom1)
            {
                _modParamId = null;
                _customValue = null;
            }
            _paramId = value;
        }
    }

    [Ignore]
    public ModulatorRtpcParameterId? ModParamId
    {
        get => _modParamId;
        set
        {
            if (value is not null)
            {
                _paramId = null;
                _customValue = null;
            }
            _modParamId = value;
        }
    }

    public virtual void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
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
                VarCount.WriteResizingUint(stream, value);
                break;
            }
        }
    }

    public virtual void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var res = DeserializeStatic(stream, context.Version, context.UseModulator);
        (ParamId, ModParamId, CustomValue) = res;
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
    
    /// <summary>
    /// Deserializes a parameter id from a stream based on the version and whether the bank uses modulator parameters
    /// </summary>
    /// <param name="stream">Stream to read from, length read may be different per version</param>
    /// <param name="version">Version of WwiseBank</param>
    /// <param name="useModulator">Whether the bank uses modulator parameters</param>
    /// <param name="isState">True if parameter being read is from HIRC State object, which uses different serialization</param>
    /// <returns>Rtpc or Modulator parameter ID, one will always be null</returns>
    /// <exception cref="Exception">Unable to read data length</exception>
    /// <exception cref="ArgumentException">No parameter ID mapping is known for given Wwise version</exception>
    public static (RtpcParameterId?, ModulatorRtpcParameterId?, byte?) DeserializeStatic(Stream stream, uint version, bool useModulator, bool isState = false)
    {
        byte value;
        switch (version)
        {
            case >= 72 and <= 126 when isState:
                value = (byte)stream.ReadByte();
                break;
            case > 126 when isState:
                Span<byte> spanUshort = stackalloc byte[2];
                var readUshort = stream.Read(spanUshort);
                if (readUshort != 2) throw new Exception();
                value = (byte)BitConverter.ToUInt16(spanUshort);
                break;
            case <= 89:
                Span<byte> spanUint = stackalloc byte[4];
                var readUint = stream.Read(spanUint);
                if (readUint != 4) throw new Exception();
                value = (byte)BitConverter.ToUInt32(spanUint);
                break;
            case <= 113:
                value = (byte)stream.ReadByte();
                break;
            default:
                value = (byte)VarCount.ReadResizingUint(stream);
                break;
        }
        
        if (version >= 118 && useModulator)
        {
            return (null, (ModulatorRtpcParameterId)value, null);
        }
        
        if (version is >= 112 and <= 113 && value is >= 0x2A and <= 0x37)
        {
            return (null, (ModulatorRtpcParameterId)(value - 0x2A), null);
        }

        var tryGetValueByVersion = ParameterIdVersionMap.V65.Value.Forward.TryGetValue;
        tryGetValueByVersion = version switch
        {
            <= 65 => ParameterIdVersionMap.V65.Value.Forward.TryGetValue,
            72 => ParameterIdVersionMap.V72.Value.Forward.TryGetValue,
            88 => ParameterIdVersionMap.V88.Value.Forward.TryGetValue,
            >= 112 and <= 113 => ParameterIdVersionMap.V112.Value.Forward.TryGetValue,
            >= 118 and < 120 => ConvertV118ToEnum,
            >= 120 and < 128 => ConvertV120ToEnum,
            >= 128 and <= 134 => ConvertV128ToEnum,
            _ => ConvertToEnumDefault,
        };
        
        if (tryGetValueByVersion(value, out var paramId))
        {
            return (paramId, null, null);
        }
        else
        {
            return (RtpcParameterId.UnknownCustom1, null, value);
        }
    }

    protected byte GetSerializableValue(uint version, bool useModulator)
    {
        if(ParamId == RtpcParameterId.UnknownCustom1 && CustomValue.HasValue)
        {
            return CustomValue.Value;
        }
        
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
            >= 135 when paramId <= (RtpcParameterId)0x3F => (byte)paramId,
            _ => throw new ArgumentException($"No known parameter id mapping for version {version}", nameof(version))
        };
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

    private static bool ConvertV118ToEnum(byte id, out RtpcParameterId paramId)
    {
        if (id > 0x2D)
        {
            paramId = RtpcParameterId.UnknownCustom1;
            return false;
        }

        if (ParameterIdVersionMap.V118.Value.Forward.TryGetValue(id, out var value))
        {
            paramId = value;
            return true;
        }

        if (Enum.IsDefined(typeof(RtpcParameterId), id))
        {
            paramId = (RtpcParameterId)id;
            return true;
        }
        paramId = RtpcParameterId.UnknownCustom1;
        return false;
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
    
    private static bool ConvertV120ToEnum(byte id, out RtpcParameterId paramId)
    {
        if (id > 0x2E || !Enum.IsDefined(typeof(RtpcParameterId), id))
        {
            paramId = RtpcParameterId.UnknownCustom1;
            return false;
        }
        paramId = (RtpcParameterId)id;
        return true;
    }
    
    private static byte ConvertV120ToByte(RtpcParameterId id)
    {
        if(id > RtpcParameterId.Positioning_EnableAttenuation)
        {
            throw new ArgumentException($"{id} is not valid for v120 - v127", nameof(id));
        }
        return (byte)id;
    }

    private static bool ConvertV128ToEnum(byte id, out RtpcParameterId paramId)
    {
        if (ParameterIdVersionMap.V128.Value.Forward.TryGetValue(id, out var value))
        {
            paramId = value;
            return true;
        }

        if (Enum.IsDefined(typeof(RtpcParameterId), id) && id <= 0x38)
        {
            paramId = (RtpcParameterId)id;
            return true;
        }

        paramId = RtpcParameterId.UnknownCustom1;
        return false;
    }
    
    
    private static bool ConvertToEnumDefault(byte id, out RtpcParameterId paramId)
    {
        if (Enum.IsDefined(typeof(RtpcParameterId), id))
        {
            paramId = (RtpcParameterId)id;
            return true;
        }

        paramId = RtpcParameterId.UnknownCustom1;
        return false;
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
        Positioning_Radius_LPF = 0x50, // v72
        Positioning_Radius_SIM_ON_OFF = 0x51, // v56<=
        Positioning_Radius_SIM_Attenuation = 0x52, // v56<=
        
        
        UnknownCustom1 = 0x99,
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

/// <summary>
/// Slightly different version of ParameterId used for State HIRC objects
/// </summary>
public class StateParameterId : ParameterId
{
    public override void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var value = GetSerializableValue(context.Version, false); // TODO: This variant should never use modulator, right?
        
        switch (context.Version)
        {
            case <= 126:
                stream.WriteByte(value);
                break;
            default:
                stream.Write(BitConverter.GetBytes((ushort)value));
                break;
        }
    }
    
    public override void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var res = DeserializeStatic(stream, context.Version, false, true);
        (ParamId, ModParamId, _) = res;
    }
}