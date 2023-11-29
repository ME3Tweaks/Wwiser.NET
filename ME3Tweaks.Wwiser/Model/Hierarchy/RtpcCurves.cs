using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class RtpcCurves
{
    //TODO: <=v36 this is a uint - not relevant to mass effect
    [FieldOrder(0)] 
    public ushort RtpcCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount(nameof(RtpcCount))]
    public List<Rtpc> Rtpcs { get; set; }
}

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
    [SerializeAs(SerializedType.UInt8)]
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

public class RtpcConversionTable
{
    [FieldOrder(8)]
    public CurveScaling Scaling { get; set; }
    
    //TODO: <=v36 this is a uint - not relevant to mass effect
    [FieldOrder(9)]
    public sbyte GraphPointCount { get; set; }
    
    [FieldOrder(10)]
    [FieldCount(nameof(GraphPointCount))]
    public List<RtpcGraphItem> Graph { get; set; }
}

public enum CurveInterpolation : uint
{
    Log3,
    Sine,
    Log1,
    InvSCurve,
    Linear,
    SCurve,
    Exp1,
    SineRecip,
    Exp3,
    Constant
}

public class RtpcGraphItem
{
    [FieldOrder(1)]
    public float From { get; set; }
    
    [FieldOrder(2)]
    public float To { get; set; }
    
    [FieldOrder(3)]
    public CurveInterpolation Interp { get; set; }
}