using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Action.Specific;
using ME3Tweaks.Wwiser.Model.RTPC;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Active : IActionParams
{
    [FieldOrder(0)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public RangedParameterInt TransitionTime { get; set; } = new();

    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public CurveInterpolation CurveInterpolation { get; set; }

    [FieldOrder(2)]
    [SubtypeFactory(nameof(Hierarchy.Action.Type), 
        typeof(ActionSpecificParamsFactory), BindingMode = BindingMode.OneWay, 
        AncestorType = typeof(Hierarchy.Action), RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public required ISpecificParams SpecificParams { get; set; }

    [FieldOrder(3)]
    [SerializeWhen(nameof(Hierarchy.Action.Type), true,
        ConverterType = typeof(HasExceptParamsConverter), BindingMode = BindingMode.OneWay, 
        AncestorType = typeof(Hierarchy.Action), RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public ExceptParams ExceptParams { get; set; } = new();
}