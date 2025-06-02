using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model;

public class PlatformChunk : Chunk
{
    public override string Tag => @"PLAT";
    
    [FieldOrder(0)]
    public BankStringUtf8 CustomPlatformName { get; set; } = new("");

    public override bool IsAllowedInVersion(uint version) => version >= 113;
}