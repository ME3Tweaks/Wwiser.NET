using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class MusicSegment : HircItem, IHasNode
{
    [FieldOrder(0)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public MusicOverrides Overrides { get; set; }
    
    [FieldOrder(1)] 
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();

    [FieldOrder(2)] 
    public Children Children { get; set; } = new();
    
    [FieldOrder(3)]
    public MeterInfo MeterInfo { get; set; } = new();
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    public bool MeterInfoFlag { get; set; }
    
    [FieldOrder(5)]
    public uint NumStingers { get; set; }
    
    [FieldOrder(6)]
    [FieldCount(nameof(NumStingers))]
    public List<Stinger> Stingers { get; set; } = new();
    
    [FieldOrder(7)]
    public double Duration { get; set; }
    
    [FieldOrder(8)]
    public uint NumMarkers { get; set; }
    
    [FieldOrder(9)]
    [FieldCount(nameof(NumMarkers))]
    public List<MusicMarker> Markers { get; set; } = new();
    
    // TODO: IsCustom thingy here
}

public class MeterInfo
{
    [FieldOrder(0)]
    public double GridPeriod { get; set; }
    
    [FieldOrder(1)]
    public double GridOffset { get; set; }
    
    [FieldOrder(2)]
    public float Tempo { get; set; }
    
    [FieldOrder(3)]
    public byte TimeSigNumBeatsBar { get; set; }
    
    [FieldOrder(4)]
    public byte TimeSigBeatValue { get; set; }
}

public class Stinger
{
    [FieldOrder(0)]
    public uint TriggerId { get; set; }
    
    [FieldOrder(1)]
    public uint SegmentId { get; set; }
    
    [FieldOrder(2)]
    public uint SyncPlayAt { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(62, ComparisonOperator.GreaterThan)]
    public uint CueFilterHash { get; set; }
    
    [FieldOrder(4)]
    public int DontRepeatTime { get; set; }
    
    [FieldOrder(5)]
    public uint NumSegmentLookAhead { get; set; }
}

public class MusicMarker : AkIdentifiable
{
    [FieldOrder(0)]
    public double Position { get; set; }

    [FieldOrder(1)] 
    [SerializeWhenVersion(62, ComparisonOperator.GreaterThan)]
    public BankStringUtf8 MarkerName { get; set; } = new("");
}