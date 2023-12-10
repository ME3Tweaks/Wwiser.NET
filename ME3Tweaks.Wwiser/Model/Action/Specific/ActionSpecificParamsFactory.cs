using System.Diagnostics.CodeAnalysis;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

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
            ActionTypeValue.Stop => typeof(Stop),
            ActionTypeValue.Pause => typeof(PauseResume),
            ActionTypeValue.Resume => typeof(PauseResume),
            _ => typeof(Action)
        };
        return true;
    }
}
