using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.GlobalSettings;

public class STMGSwitchGroup : AkIdentifiable
{
    [FieldOrder(0)]
    public uint RtpcID { get; set; } // I don't know what this does. Line 3598 of wparser.py

    [FieldOrder(1)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public RtpcType? RtpcType { get; set; }
    
    [FieldOrder(2)]
    public uint GraphCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(GraphCount))]
    public List<RtpcGraphItem> Graph { get; set; } = new();
    
}