using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class PlatformChunk : Chunk
{
    public override string Tag => @"PLAT";
    
    [FieldOrder(0)]
    public BankStringUtf8 CustomPlatformName { get; set; }
}