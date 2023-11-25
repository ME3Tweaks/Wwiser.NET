using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model.Plugins;

public class EmptyPluginParams : IPluginParams
{
    [FieldOrder(0)]
    [FieldCount(nameof(FxBase.ParamSize), AncestorType = typeof(FxBase), AncestorLevel = 2)]
    public byte[] Data { get; set; } = Array.Empty<byte>();
}

