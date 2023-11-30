using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class RtpcConversionTable
{
    [FieldOrder(8)]
    public CurveScaling Scaling { get; set; }
    
    //TODO: <=v36 this is a uint - not relevant to mass effect
    [FieldOrder(9)]
    public ushort GraphPointCount { get; set; }
    
    [FieldOrder(10)]
    [FieldCount(nameof(GraphPointCount))]
    public List<RtpcGraphItem> Graph { get; set; }
}