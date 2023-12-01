using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class UnionRange
{
    [FieldOrder(0)]
    public Uni Low { get; set; }
    
    [FieldOrder(1)]
    public Uni High { get; set; }
}