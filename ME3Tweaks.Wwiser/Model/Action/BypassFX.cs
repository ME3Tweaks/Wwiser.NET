using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action;

public class BypassFX
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsBypass{ get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersionBetween(27, 145)]
    public byte TargetMask { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public byte ByFxSlot { get; set; }
    
    [FieldOrder(4)]
    public ExceptParams ExceptParams { get; set; } = new();
}