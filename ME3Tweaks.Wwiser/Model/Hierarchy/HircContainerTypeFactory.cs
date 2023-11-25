using System.Diagnostics.CodeAnalysis;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model;

public class HircContainerTypeFactory : ISubtypeFactory
{
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        var version = (uint)key;
        type = version switch
        {
            <= 48 => typeof(HircItemContainer),
            < 128 => typeof(HircItemContainerV49),
            _ => typeof(HircItemContainerV128)
        };

        return true;
    }
}