using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class Plugin
{
    // Todo: Convert to enums, company/plugin getters and setters maybe use custom serialization?
    [FieldOrder(0)]
    public uint PluginId { get; set; }
    
    [Ignore]
    public ushort CompanyId { get; set; }
    
    [Ignore]
    public ushort PluginType { get; set; }
}