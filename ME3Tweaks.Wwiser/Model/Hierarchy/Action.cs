﻿using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.Action;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Action : HircItem
{
    [FieldOrder(0)] 
    public ActionType Type { get; set; } = new();
    
    [FieldOrder(1)]
    public uint TargetId { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int Delay { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int DelayMin { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int DelayMax { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhenVersion(65, ComparisonOperator.GreaterThan)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsBus { get; set; }

    [FieldOrder(6)]
    [SerializeWhenVersion(56, ComparisonOperator.GreaterThan)]
    public InitialParamsV62 PropBundle { get; set; } = new();

    [FieldOrder(7)]
    [SubtypeFactory($"{nameof(Type)}.{nameof(Type.Value)}", typeof(ActionParamsFactory),
        BindingMode = BindingMode.OneWay)]
    public required IActionParams ActionParams { get; set; }
}