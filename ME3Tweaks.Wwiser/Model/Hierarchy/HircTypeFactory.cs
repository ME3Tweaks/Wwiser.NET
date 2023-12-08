using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircTypeFactory : ISubtypeFactory
{
    private static readonly Dictionary<Type, HircType> TypeToEnum = new()
    {
        { typeof(Event), HircType.Event },
        { typeof(Attenuation), HircType.Attenuation },
        { typeof(FxShareSet), HircType.FxShareSet },
        { typeof(FxCustom), HircType.FxCustom },
        { typeof(Sound), HircType.Sound },
        { typeof(ActorMixer), HircType.ActorMixer },
    };
    
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        if (TypeToEnum.TryGetValue(valueType, out var value))
        {
            key = value;
            return true;
        }

        // fallback
        key = HircType.Action;
        return false;
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        if (key is byte b)
        {
            key = (uint)b; // explicit cast from byte to uint is required here for... some reason
        }
        
        type = (HircType)key switch
        {
            HircType.Event => typeof(Event),
            HircType.Attenuation => typeof(Attenuation),
            HircType.FxShareSet => typeof(FxShareSet),
            HircType.FxCustom => typeof(FxCustom),
            HircType.Sound => typeof(Sound),
            HircType.ActorMixer => typeof(ActorMixer),
            _ => typeof(HircItem)
        };
        return true;
    }
}
