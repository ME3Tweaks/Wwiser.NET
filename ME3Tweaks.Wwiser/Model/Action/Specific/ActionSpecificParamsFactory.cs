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
            >= ActionTypeValue.SetPitch1 and <= ActionTypeValue.SetLPF2 => typeof(SetAkProp), // TODO: Verify this is correct for <= v56
            ActionTypeValue.SetHPF1 => typeof(SetAkProp),
            ActionTypeValue.SetHPF2 => typeof(SetAkProp),
            ActionTypeValue.SetGameParameter1 => typeof(SetGameParameter),
            ActionTypeValue.SetGameParameter2 => typeof(SetGameParameter),
            ActionTypeValue.ResetPlaylist => typeof(ResetPlaylist),
            _ => typeof(Action)
        };
        return true;
    }
}