using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateProp
{
    [FieldOrder(0)]
    public VarCount PropertyId { get; set; } = new();
    
    [FieldOrder(1)]
    public AccumType AccumType { get; set; } = new();
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(126, ComparisonOperator.GreaterThan)]
    public bool InDb { get; set; }
}