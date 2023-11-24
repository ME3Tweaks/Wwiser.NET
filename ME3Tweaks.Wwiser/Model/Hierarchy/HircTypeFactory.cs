using System.Diagnostics.CodeAnalysis;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircTypeFactory : ISubtypeFactory
{ 
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        throw new NotImplementedException();
    }
}

public class HircTypeFactory128 : ISubtypeFactory
{ 
    public bool TryGetKey(Type valueType, [UnscopedRef] out object key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetType(object key, [UnscopedRef] out Type type)
    {
        throw new NotImplementedException();
    }
}
