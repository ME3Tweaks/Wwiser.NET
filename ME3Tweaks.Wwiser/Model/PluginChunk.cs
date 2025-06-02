using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model;

public class PluginChunk : Chunk
{
    public override string Tag => @"INIT";
    
    [FieldOrder(0)]
    public uint PluginCount { get; set; }
    
    [FieldOrder(1)]
    [FieldLength(nameof(PluginCount))]
    public List<AKPlugin> AKPluginList { get; set; } = new();
    
    public override bool IsAllowedInVersion(uint version) => version >= 118;
}

public class AKPlugin
{
    [FieldOrder(0)]
    public Plugin Plugin { get; set; } = new();
    
    [FieldOrder(1)]
    public BankStringUtf8 DLLName { get; set; } = new("");
}