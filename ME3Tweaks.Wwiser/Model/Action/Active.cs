using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Active : IActionParams
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }

    [FieldOrder(2)] 
    [FieldLength(nameof(SubSectionSize), BindingMode = BindingMode.OneWayToSource)]
    public required ActiveParams Params { get; set; }
}