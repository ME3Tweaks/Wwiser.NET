using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class PauseResume
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    [SerializeAs(SerializedType.UInt4)]
    public bool IsMaster { get; set; } // TODO: This is a byte on V62 - not relevant to mass effect
    
    [FieldOrder(2)]
    [FieldLength(0x0C)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public byte[] Data { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(62, ComparisonOperator.GreaterThan)]
    public ActiveFlags Flags { get; set; }
}