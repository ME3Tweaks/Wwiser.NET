using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Play : ActionParams
{
    [FieldOrder(0)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int Time { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int TimeMin { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public int TimeMax { get; set; }
    
    [FieldOrder(3)]
    [SerializeAs(SerializedType.UInt1)]
    public CurveInterpolation CurveInterpolation { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public SpecificParams SpecificParams { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public ExceptParams ExceptParams { get; set; }
    
    [FieldOrder(6)]
    [SerializeWhenVersion(26, ComparisonOperator.GreaterThan)]
    public uint BankId { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhenVersion(144, ComparisonOperator.GreaterThanOrEqual)]
    public uint BankType { get; set; }
}