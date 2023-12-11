using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Action.Specific;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActiveParams
{
    [FieldOrder(0)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public RangedParameterInt Time { get; set; } = new();

    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public CurveInterpolation CurveInterpolation { get; set; }

    [FieldOrder(2)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    [SubtypeFactory($"{nameof(Hierarchy.Action.Type)}.{nameof(Hierarchy.Action.Type.Value)}", 
        typeof(ActionSpecificParamsFactory), BindingMode = BindingMode.OneWay,
        AncestorType = typeof(Hierarchy.Action), RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public required ISpecificParams SpecificParams { get; set; }

    [FieldOrder(3)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public ExceptParams ExceptParams { get; set; } = new();
}