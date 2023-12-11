using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class RangedParameterInt
{
    [FieldOrder(0)]
    public int Base { get; set; }
    
    [FieldOrder(1)]
    public int Min { get; set; }
    
    [FieldOrder(2)]
    public int Max { get; set; }
}