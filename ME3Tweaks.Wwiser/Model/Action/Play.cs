using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Play : IActionParams
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }

    [FieldOrder(2)] 
    [FieldLength(nameof(SubSectionSize), BindingMode = BindingMode.OneWayToSource)]
    public required ActiveParamsPlay Params { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(26, ComparisonOperator.GreaterThan)]
    public uint BankId { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(144, ComparisonOperator.GreaterThanOrEqual)]
    public uint BankType { get; set; }
}