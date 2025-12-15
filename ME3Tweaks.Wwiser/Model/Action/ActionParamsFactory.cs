using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActionParamsFactory : ISubtypeFactory
{
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        type = (ActionTypeValue)key switch
        {
            ActionTypeValue.SetState => typeof(SetState),
            ActionTypeValue.SetSwitch => typeof(SetSwitch),
            ActionTypeValue.SetRTPC => typeof(SetRTPC),
            ActionTypeValue.SetFX1 => typeof(SetFX),
            ActionTypeValue.SetFX2 => typeof(SetFX),
            ActionTypeValue.BypassFX1 => typeof(BypassFX),
            ActionTypeValue.BypassFX2 => typeof(BypassFX),
            ActionTypeValue.BypassFX3 => typeof(BypassFX),
            ActionTypeValue.BypassFX4 => typeof(BypassFX),
            ActionTypeValue.BypassFX5 => typeof(BypassFX),
            ActionTypeValue.BypassFX6 => typeof(BypassFX),
            ActionTypeValue.BypassFX7 => typeof(BypassFX),
            ActionTypeValue.Seek => typeof(Seek),
            ActionTypeValue.Release => typeof(Empty),
            ActionTypeValue.PlayEvent => typeof(Empty),
            ActionTypeValue.UseState1 => typeof(UseState),
            ActionTypeValue.UseState2 => typeof(UseState),
            >= ActionTypeValue.Event1 and <= ActionTypeValue.Event3 => typeof(Empty),
            ActionTypeValue.Duck => typeof(Empty),
            ActionTypeValue.Break => typeof(Empty),
            ActionTypeValue.Trigger => typeof(Empty),
            _ => typeof(Active)
        };
        return true;
    }
}