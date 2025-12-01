using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemSubtypeFactory : ISubtypeFactory
{
    private static readonly Dictionary<Type, HircType> TypeToEnum = new()
    {
        { typeof(State), HircType.State },
        { typeof(Sound), HircType.Sound },
        { typeof(Action), HircType.Action },
        { typeof(Event), HircType.Event },
        { typeof(RandSeqContainer), HircType.RandomSequenceContainer },
        { typeof(SwitchContainer), HircType.SwitchContainer },
        { typeof(ActorMixer), HircType.ActorMixer },
        { typeof(LayerContainer), HircType.LayerContainer },
        { typeof(Attenuation), HircType.Attenuation },
        { typeof(FxShareSet), HircType.FxShareSet },
        { typeof(FxCustom), HircType.FxCustom },
    };
    
    public bool TryGetKey(Type valueType, out object? key)
    {
        if (TypeToEnum.TryGetValue(valueType, out var value))
        {
            key = value;
            return true;
        }

        key = null;
        return false;
    }

    public bool TryGetType(object key, [UnscopedRef] out Type? type)
    {
        if (key is byte b)
        {
            key = (uint)b; // explicit cast from byte to uint is required here for... some reason
        }
        
        type = (HircType)key switch
        {
            HircType.State => typeof(State),
            HircType.Sound => typeof(Sound),
            HircType.Action => typeof(Action),
            HircType.Event => typeof(Event),
            HircType.RandomSequenceContainer => typeof(RandSeqContainer),
            HircType.SwitchContainer => typeof(SwitchContainer),
            HircType.ActorMixer => typeof(ActorMixer),
            //HircType.Bus =>
            HircType.LayerContainer => typeof(LayerContainer),
            //HircType.MusicSegment =>
            //HircType.MusicTrack =>
            //HircType.MusicSwitch =>
            //HircType.MusicRandomSequence =>
            HircType.Attenuation => typeof(Attenuation),
            //HircType.DialogueEvent =>
            //HircType.FeedbackBus =>
            //HircType.FeedbackNode =>
            HircType.FxShareSet => typeof(FxShareSet),
            HircType.FxCustom => typeof(FxCustom),
            //HircType.AuxiliaryBus =>
            //HircType.LFO =>
            //HircType.Envelope =>
            //HircType.AudioDevice =>
            //HircType.TimeMod =>
            _ => null
        };
        return type != null;
    }
}
