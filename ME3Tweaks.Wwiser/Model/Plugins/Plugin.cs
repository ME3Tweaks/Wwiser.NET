using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Plugins;

public class Plugin
{
    [FieldOrder(0)]
    public uint PluginId { get; set; }

    [Ignore] 
    public ushort CompanyId => (ushort)((PluginId >> 4) & 0x03FF);

    [Ignore]
    public ushort PluginType => (ushort)((PluginId >> 0) & 0x000F);
}