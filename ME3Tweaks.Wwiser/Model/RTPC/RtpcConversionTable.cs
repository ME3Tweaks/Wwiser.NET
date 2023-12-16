using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class RtpcConversionTable
{
    [FieldOrder(8)]
    public CurveScaling Scaling { get; set; } = new();
    
    [FieldOrder(9)] 
    public V36Count GraphPointCount { get; set; } = new();

    [FieldOrder(10)]
    [FieldCount($"{nameof(GraphPointCount)}.{nameof(GraphPointCount.Value)}")]
    public List<RtpcGraphItem> Graph { get; set; } = new();
}