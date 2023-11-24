using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircTypeFactory : ISubtypeFactory
{
    private static readonly Dictionary<Type, HircType> _typeToEnum = new()
    {
        { typeof(HircEventItem), HircType.Event }
    };
    
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        if (_typeToEnum.TryGetValue(valueType, out var value))
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
        type = key switch
        {
            HircType.Event => typeof(HircEventItem),
            _ => typeof(HircItem)
        };
        return true;
    }
}

public class HircTypeFactory128 : ISubtypeFactory
{ 
    private static readonly Dictionary<Type, HircType128> _typeToEnum = new()
    {
        { typeof(HircEventItem), HircType128.Event }
    };
    
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        if (_typeToEnum.TryGetValue(valueType, out var value))
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
            HircType128.Event => typeof(HircEventItem),
            _ => typeof(HircItem)
        };
        return true;
    }
}
