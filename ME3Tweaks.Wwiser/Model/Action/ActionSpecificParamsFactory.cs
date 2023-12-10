using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActionSpecificParamsFactory : ISubtypeFactory
{
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        type = (ActionTypeValue)key switch
        {
            ActionTypeValue.Play => typeof(Specific.Action),
            _ => typeof(Play)
        };
        return true;
    }
}
