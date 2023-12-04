using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Plugins;

public class PluginParameters
{
    [FieldOrder(1)]
    public uint ParamSize { get; set; }

    [FieldOrder(2)]
    [FieldLength(nameof(ParamSize))]
    [SubtypeDefault(typeof(EmptyPluginParams))]
    public IPluginParams PluginParams { get; set; } = new EmptyPluginParams();
}