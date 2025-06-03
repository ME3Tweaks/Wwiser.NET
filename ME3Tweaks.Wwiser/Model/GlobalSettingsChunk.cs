using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.GlobalSettings;

namespace ME3Tweaks.Wwiser.Model;

public class GlobalSettingsChunk : Chunk
{
    public override string Tag => @"STMG";
    
    [FieldOrder(0)]
    [SerializeWhenVersion(140, ComparisonOperator.GreaterThan)]
    public FilterBehavior FilterBehavior { get; set; }

    [FieldOrder(2)]
    public float VolumeThreshold { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(53, ComparisonOperator.GreaterThan)]
    public ushort MaxNumVoicesLimitInternal { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(126, ComparisonOperator.GreaterThan)]
    public ushort MaxNumDangerousVirtVoicesLimitInternal { get; set; }
    
    [FieldOrder(5)]
    public uint StateGroupCount { get; set; }
    
    [FieldOrder(6)]
    [FieldCount(nameof(StateGroupCount))]
    public List<STMGStateGroup> StateGroups { get; set; } = new();
    
    [FieldOrder(7)]
    public uint SwitchGroupCount { get; set; }
    
    [FieldOrder(8)]
    [FieldCount(nameof(SwitchGroupCount))]
    public List<STMGSwitchGroup> SwitchGroups { get; set; } = new();
    
    [FieldOrder(9)]
    [SerializeWhenVersion(38, ComparisonOperator.GreaterThan)]
    public uint ParamCount { get; set; }
    
    [FieldOrder(10)]
    [FieldCount(nameof(ParamCount))]
    [SerializeWhenVersion(38, ComparisonOperator.GreaterThan)]
    public List<STMGParam> Params { get; set; } = new();

    [FieldOrder(11)]
    [SerializeWhenVersion(118, ComparisonOperator.GreaterThan)]
    public STMGAcousticTextures Textures { get; set; } = new();
    
    [FieldOrder(12)]
    [SerializeWhenVersionBetween(119, 122)]
    public uint ReverberatorCount { get; set; }
    
    [FieldOrder(13)]
    [FieldCount(nameof(ReverberatorCount))]
    [SerializeWhenVersionBetween(119, 122)]
    public List<STMGReverberator> Reverberators { get; set; } = new();
}

public enum FilterBehavior : short
{
    Additive = 0,
    Maximum = 1,
}