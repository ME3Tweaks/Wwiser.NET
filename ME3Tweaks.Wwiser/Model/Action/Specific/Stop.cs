using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class Stop : ISpecificParams
{
    [FieldOrder(0)]
    [FieldLength(0x10)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public byte[] Data { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhenVersion(122, ComparisonOperator.GreaterThan)]
    public ActiveFlags Flags { get; set; }
}