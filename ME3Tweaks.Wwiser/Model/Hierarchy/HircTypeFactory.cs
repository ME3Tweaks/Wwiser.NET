using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircTypeFactory : ISubtypeFactory
{
    private static readonly Dictionary<Type, HircType> TypeToEnum = new()
    {
        { typeof(Event), HircType.Event },
        { typeof(FxShareSet), HircType.FxShareSet },
        { typeof(FxCustom), HircType.FxCustom}
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
            HircType.FxShareSet => typeof(FxShareSet),
            HircType.FxCustom => typeof(FxCustom),
            _ => typeof(HircItem)
        };
        return true;
    }
}

public class HircTypeFactory128 : ISubtypeFactory
{ 
    private static readonly Dictionary<Type, HircType128> TypeToEnum = new()
    {
        { typeof(Event), HircType128.Event },
        { typeof(FxShareSet), HircType128.FxShareSet },
        { typeof(FxCustom), HircType128.FxCustom}
    };
    
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        if (TypeToEnum.TryGetValue(valueType, out var value))
        {
            key = value;
            return true;
        }

        // fallback
        key = HircType128.Action;
        return false;
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        type = key switch
        {
            HircType128.Event => typeof(Event),
            HircType128.FxShareSet => typeof(FxShareSet),
            HircType128.FxCustom => typeof(FxCustom),
            _ => typeof(HircItem)
        };
        return true;
    }
}
