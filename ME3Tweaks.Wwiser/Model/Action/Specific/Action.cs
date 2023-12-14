using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class Action : ISpecificParams
{
    [FieldOrder(0)]
    [FieldLength(0x10)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public byte[] Data { get; set; } = Array.Empty<byte>();
}