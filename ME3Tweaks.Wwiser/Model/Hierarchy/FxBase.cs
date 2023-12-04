using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Plugins;
using ME3Tweaks.Wwiser.Model.RTPC;
using ME3Tweaks.Wwiser.Model.State;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

// These are exactly the same
public class FxShareSet : FxBase { }
public class FxCustom : FxBase { }

public class FxBase : HircItem
{
    [FieldOrder(0)]
    public required Plugin Plugin { get; set; }
    
    [FieldOrder(1)]
    public PluginParameters PluginParameters { get; set; }
    
    // Turn into class - AKMediaMap???
    [FieldOrder(3)]
    public byte BankDataCount { get; set; }
    
    [FieldOrder(4)]
    [FieldCount(nameof(BankDataCount))]
    public List<MediaMapItem> Media { get; set; }
    
    [FieldOrder(5)]
    public RtpcCurves RtpcCurves { get; set; }
    
    [FieldOrder(6)]
    [SerializeWhenVersionBetween(123, 126)]
    public ushort Unk1 { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhenVersion(126, ComparisonOperator.GreaterThan)]
    public StateChunk_Aware StateChunk { get; set; }
    
    [FieldOrder(8)]
    [SerializeWhenVersion(90, ComparisonOperator.GreaterThan)]
    public ushort RtpcInitCount { get; set; }
    
    [FieldOrder(9)]
    [FieldCount(nameof(RtpcInitCount))]
    [SerializeWhenVersion(90, ComparisonOperator.GreaterThan)]
    public List<RtpcInitValue> RtpcInitValues { get; set; }
    
    
    public class RtpcInitValue
    {
        [FieldOrder(0)]
        public VarCount ParameterId { get; set; }
        
        [FieldOrder(1)]
        [SerializeWhenVersion(126, ComparisonOperator.GreaterThan)]
        public byte RtpcAccum { get; set; }
        
        [FieldOrder(2)]
        public float InitValue { get; set; }
    }
}

public class MediaMapItem
{
    [FieldOrder(0)]
    public byte Index { get; set; }
    
    [FieldOrder(1)]
    public uint SourceId { get; set;  }
}

