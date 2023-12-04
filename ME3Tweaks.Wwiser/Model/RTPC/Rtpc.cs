using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class Rtpc
{
    [FieldOrder(0)]
    [SerializeWhenVersion(48, ComparisonOperator.LessThanOrEqual)]
    public Plugin Plugin { get; set; } = new();
    
    [FieldOrder(1)]
    [SerializeWhenVersion(48, ComparisonOperator.LessThanOrEqual)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsRendered { get; set; }
    
    [FieldOrder(3)]
    public uint RtpcId { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public RtpcType RtpcType { get; set; } = new();
    
    [FieldOrder(5)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public AccumType RtpcAccum { get; set; } = new();
    
    [FieldOrder(6)]
    public ParameterId ParamId { get; set; } = new();
    
    [FieldOrder(7)]
    public uint RtpcCurveId { get; set; }
    
    [FieldOrder(8)]
    public RtpcConversionTable RtpcConversionTable { get; set; } = new();
}