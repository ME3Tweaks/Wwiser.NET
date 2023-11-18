using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class Plugin
{
    // TODO: Add enum to resolve values?
    [FieldOrder(0)]
    public UInt16 PluginId { get; set; }
    
    // Todo: Implement 12 byte id, 4 byte plugin type to enums
    [FieldOrder(1)]
    [FieldLength(12)]
    public byte[] CompanyId { get; set; }
    
    [FieldOrder(2)]
    [FieldLength(4)]
    public byte[] PluginType { get; set; }
}