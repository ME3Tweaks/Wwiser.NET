using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetSwitch
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(2)]
    public uint SwitchGroupId { get; set; }
    
    [FieldOrder(3)]
    public uint SwitchStateId { get; set; }
}