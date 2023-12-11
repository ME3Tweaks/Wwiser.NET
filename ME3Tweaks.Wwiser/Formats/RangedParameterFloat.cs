using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class RangedParameterFloat
{
    [FieldOrder(0)]
    public float Base { get; set; }
    
    [FieldOrder(1)]
    public float Min { get; set; }
    
    [FieldOrder(2)]
    public float Max { get; set; }
}