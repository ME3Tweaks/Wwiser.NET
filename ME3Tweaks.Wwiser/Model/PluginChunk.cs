using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model;

public class PluginChunk : Chunk
{
    public override string Tag => @"INIT";
    
    [FieldOrder(0)]
    public UInt32 PluginCount { get; set; }
    
    [FieldOrder(1)]
    [FieldLength(nameof(PluginCount))]
    public List<AKPLugin> AKPluginList { get; set; }
}

public class AKPLugin
{
    [FieldOrder(0)]
    public Plugin Plugin { get; set; }
    
    [FieldOrder(1)]
    public BankStringUtf8 DLLName { get; set; }
}