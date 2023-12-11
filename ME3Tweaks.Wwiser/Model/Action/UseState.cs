﻿using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Action.Specific;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Action;

public class UseState
{
    [FieldOrder(0)]
    [SerializeWhenVersion(56)]
    public RangedParameterInt Time { get; set; } = new();

    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(56)]
    public CurveInterpolation CurveInterpolation { get; set; }

    [FieldOrder(2)]
    [SerializeWhenVersion(56)]
    [SubtypeFactory($"{nameof(Hierarchy.Action.Type)}.{nameof(Hierarchy.Action.Type.Value)}", 
        typeof(ActionSpecificParamsFactory), BindingMode = BindingMode.OneWay,
        AncestorType = typeof(Hierarchy.Action), RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public required ISpecificParams SpecificParams { get; set; }

    [FieldOrder(3)]
    [SerializeWhenVersion(56)]
    public ExceptParams ExceptParams { get; set; } = new();
}