namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public enum HircType128 : byte
{
    State,
    Sound,
    Action,
    Event,
    RandomSequenceContainer,
    SwitchContainer,
    ActorMixer,
    Bus,
    LayerContainer,
    MusicSegment,
    MusicTrack,
    MusicSwitch,
    MusicRandomSequence,
    Attenuation,
    DialogueEvent,
    FxShareSet,
    FxCustom,
    AuxiliaryBus,
    LFO,
    Envelope,
    AudioDevice,
    TimeMod,
}

public enum HircType : byte
{
    State,
    Sound,
    Action,
    Event,
    RandomSequenceContainer,
    SwitchContainer,
    ActorMixer,
    Bus,
    LayerContainer,
    MusicSegment,
    MusicTrack,
    MusicSwitch,
    MusicRandomSequence,
    Attenuation,
    DialogueEvent,
    FeedbackBus,
    FeedbackNode,
    FxShareSet,
    FxCustom,
    AuxiliaryBus,
    LFO,
    Envelope,
    AudioDevice,
}

public static class HircTypeExtensions
{
    public static HircType ToHircType(this HircType128 type)
    {
        if (type is HircType128.TimeMod)
        {
            throw new NotSupportedException($"Cannot convert type {type}");
        }
        
        if (type >= HircType128.FxShareSet)
        {
            return (HircType)(type + 2);
        }
        return (HircType)type;
    }

    public static HircType128 ToHircType128(this HircType type)
    {
        if (type is HircType.FeedbackBus or HircType.FeedbackNode)
        {
            throw new NotSupportedException($"Cannot convert type {type}");
        }

        if (type >= HircType.FxShareSet)
        {
            return (HircType128)(type - 2);
        }
        return (HircType128)type;
    }
}