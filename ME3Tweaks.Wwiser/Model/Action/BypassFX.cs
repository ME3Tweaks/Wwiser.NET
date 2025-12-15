using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class BypassFX : IActionParams
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsBypass{ get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersionBetween(27, 145)]
    public byte TargetMask { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public byte ByFxSlot { get; set; }
    
    [FieldOrder(4)]
    public ExceptParams ExceptParams { get; set; } = new();
}