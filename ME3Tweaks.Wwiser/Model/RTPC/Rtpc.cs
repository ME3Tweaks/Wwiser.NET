﻿using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class Rtpc
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public Plugin Plugin { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsRendered { get; set; }
    
    [FieldOrder(3)]
    public uint RtpcId { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 89,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public RtpcType RtpcType { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 89,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public AccumType RtpcAccum { get; set; }
    
    [FieldOrder(6)]
    public ParameterId ParamId { get; set; }
    
    [FieldOrder(7)]
    public uint RtpcCurveId { get; set; }
    
    [FieldOrder(8)]
    public RtpcConversionTable RtpcConversionTable { get; set; }
}