using BinarySerialization;
using ME3Tweaks.Wwiser.Model.FXPR;

namespace ME3Tweaks.Wwiser.Model;

public class FxParamChunk: Chunk
{
    public override string Tag => @"FXPR";
    
    [FieldOrder(0)]
    public uint ParameterCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount(nameof(ParameterCount))]
    public List<FxParameter> Parameters { get; set; } = new();

    public override bool IsAllowedInVersion(uint version)
    {
        return version is >= 26 and <= 48;
    }
}