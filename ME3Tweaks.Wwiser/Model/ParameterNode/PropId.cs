using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class SmartPropId : IBinarySerializable
{
    public PropId Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var id = Value;
        if (version == 113)
        {
            id = SerializeVersion113(id);
        }
        else if (version <= 65)
        {
            id = SerializeVersionLte65(id);
        }
        else if (version <= 112)
        {
            id = SerializeVersionLte112(id);
        }
        else if (version <= 125)
        {
            id = SerializeVersionLte125(id);
        }
        stream.WriteByte((byte)id);
    }
    
    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var id = (PropId)stream.ReadByte();
        if (version == 113)
        {
            id = DeserializeVersion113(id);
        }
        else if (version <= 65)
        {
            id = DeserializeVersionLte65(id);
        } 
        else if (version <= 112)
        {
            id = DeserializeVersionLte112(id);
        }
        else if (version <= 125)
        {
            id = DeserializeVersionLte125(id);
        }
        Value = id;
    }

    private static PropId SerializeVersion113(PropId input)
    {
        return input switch
        {
            PropId.Loop => (PropId)0x3A,
            PropId.InitialDelay => (PropId)0x3B,
            > PropId.InitialDelay => input - 2,
            > PropId.PriorityDistanceOffset => input - 1,
            _ => input
        };
    }

    private static PropId DeserializeVersion113(PropId input)
    {
        return input switch
        {
            (PropId)0x3A => PropId.Loop,
            (PropId)0x3B => PropId.InitialDelay,
            >= PropId.HDRBusThreshold => input + 2,
            >= PropId.Loop => input + 1,
            _ => input
        };
    }
    
    private static PropId DeserializeVersionLte125(PropId input)
    {
        return input switch
        {
            (PropId)0x3A => PropId.Loop,
            (PropId)0x3B => PropId.InitialDelay,
            (PropId)0x06 => PropId.MakeUpGain,
            >= PropId.HDRActiveRange => input + 2,
            >= PropId.InitialDelay => input + 1,
            >= PropId.FeedbackVolume => input,
            >= PropId.Priority => input - 1,
            _ => input
        };
    }

    private static PropId SerializeVersionLte125(PropId input)
    {
        return input switch
        {
            PropId.Loop => (PropId)0x3A,
            PropId.InitialDelay => (PropId)0x3B,
            PropId.MakeUpGain => (PropId)0x06,
            > PropId.MakeUpGain => input - 2,
            > PropId.InitialDelay => input - 1,
            > PropId.Loop=> input,
            > PropId.BusVolume => input + 1,
            _ => input
        };
    }

    private static PropId DeserializeVersionLte112(PropId input)
    {
        return input switch
        {
            >= PropId.OutputBusVolume => input + 2,
            >= PropId.HPF => input + 1,
            _ => input
        };
    }

    private static PropId SerializeVersionLte112(PropId input)
    {
        return input switch
        {
            >= PropId.OutputBusHPF => input - 2,
            >= PropId.HPF => input - 1,
            _ => input
        };
    }

    protected virtual PropId DeserializeVersionLte65(PropId input)
    {
        return input switch
        {
            (PropId)0x18 => PropId.OutputBusLPF,
            > (PropId)0x0F => input + 1,
            > PropId.LPF => input + 2,
            _ => input
        };
    }

    private static PropId SerializeVersionLte65(PropId input)
    {
        
        return input switch
        {
            PropId.OutputBusLPF => (PropId)0x18,
            >= PropId.DialogueMode => input - 1,
            >= PropId.BusVolume => input - 2,
            _ => input
        };
    }
}

/// <summary>
/// Represents a property type. DO NOT SERIALIZE THIS ENUM ALONE, it is not valid
/// for ANY version of Wwise. Use SmartPropId to convert to appropriate version.
/// </summary>
public enum PropId : byte
{
    Volume,
    LFE,
    Pitch,
    LPF,
    HPF,
    BusVolume,
    Priority,
    PriorityDistanceOffset,
    Loop,
    FeedbackVolume,
    FeedbackLPF,
    MuteRatio,
    PAN_LR,
    PAN_FR,
    CenterPCT,
    DelayTime,
    TransitionTime,
    Probability,
    DialogueMode,
    UserAuxSendVolume0,
    UserAuxSendVolume1,
    UserAuxSendVolume2,
    UserAuxSendVolume3,
    GameAuxSendVolume,
    OutputBusVolume,
    OutputBusHPF,
    OutputBusLPF,
    InitialDelay,
    HDRBusThreshold,
    HDRBusRatio,
    HDRBusReleaseTime,
    HDRBusGameParam,
    HDRBusGameParamMin,
    HDRBusGameParamMax,
    HDRActiveRange,
    MakeUpGain,
    LoopStart,
    LoopEnd,
    TrimInTime,
    TrimOutTime,
    FadeInTime,
    FadeOutTime,
    FadeInCurve,
    FadeOutCurve,
    LoopCrossfadeDuration,
    CrossfadeUpCurve,
    CrossfadeDownCurve,
    MidiTrackingRootNote,
    MidiPlayOnNoteType,
    MidiTransposition,
    MidiVelocityOffset,
    MidiKeyRangeMin,
    MidiKeyRangeMax,
    MidiVelocityRangeMn,
    MidiVelocityRangeMx,
    MidiChannelMask,
    PlaybackSpeed,
    MidiTempoSource,
    MidiTargetNode,
    AttachedPluginFXID,
}