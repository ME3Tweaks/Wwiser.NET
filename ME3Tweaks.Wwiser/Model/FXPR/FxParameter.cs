using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.Plugins;
using ME3Tweaks.Wwiser.Model.RTPC;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.FXPR;

public class FxParameter
{
    [FieldOrder(0)]
    public uint EnvId { get; set; }
    
    [FieldOrder(1)]
    public Plugin Plugin { get; set; }
    
    [FieldOrder(2)]
    public uint PresetSize { get; set; }
    
    [FieldOrder(3)]
    [FieldLength(nameof(PresetSize))]
    public byte[] PresetData { get; set; } = [];
    
    [FieldOrder(4)]
    [SerializeWhenVersion(47, ComparisonOperator.GreaterThanOrEqual)]
    public uint BankDataCount { get; set; }

    [FieldOrder(5)]
    [FieldCount(nameof(BankDataCount))]
    [SerializeWhenVersion(47, ComparisonOperator.GreaterThanOrEqual)]
    public List<BankDataFXParam> BankParams { get; set; } = new();
    
    [FieldOrder(6)]
    public ushort RTPCCount { get; set; }

    [FieldOrder(7)]
    [FieldCount(nameof(RTPCCount))]
    public List<RTPCFxParameter> RTPCParams { get; set; } = new();

}

public class BankDataFXParam
{
    [FieldOrder(0)]
    public uint FXParameterSetId { get; set; }
    
    [FieldOrder(1)]
    public uint Item { get; set; }
}

public class RTPCFxParameter
{
    [FieldOrder(1)] 
    public uint BufferToFill { get; set; }
    
    [FieldOrder(2)]
    public byte FXIndex { get; set; }
    
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