using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Plugins;

public class EmptyPluginParams : IPluginParams
{
    [FieldOrder(0)]
    [FieldCount(nameof(PluginParameters.ParamSize), 
        RelativeSourceMode = RelativeSourceMode.FindAncestor, AncestorType = typeof(PluginParameters))]
    public byte[] Data { get; set; } = Array.Empty<byte>();
}

