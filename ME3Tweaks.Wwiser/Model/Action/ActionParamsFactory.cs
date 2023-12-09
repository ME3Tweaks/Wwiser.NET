using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActionParamsFactory : ISubtypeFactory
{
    private static readonly Dictionary<Type, ActionTypeValue> TypeToEnum = new()
    {
        { typeof(Play), ActionTypeValue.Play},
    };
    
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        if (TypeToEnum.TryGetValue(valueType, out var value))
        {
            key = value;
            return true;
        }

        // fallback
        key = ActionTypeValue.Play;
        return false;
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        type = (ActionTypeValue)key switch
        {
            ActionTypeValue.Play => typeof(Play),
            _ => typeof(Play)
        };
        return true;
    }
}
