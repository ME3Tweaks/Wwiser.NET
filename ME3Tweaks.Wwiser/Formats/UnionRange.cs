using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class UnionRange
{
    [FieldOrder(0)]
    public required Uni Low { get; set; }
    
    [FieldOrder(1)]
    public required Uni High { get; set; }
}