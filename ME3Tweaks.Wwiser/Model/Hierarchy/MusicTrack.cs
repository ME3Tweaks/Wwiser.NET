using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class MusicTrack : HircItem, IHasNode
{
    [FieldOrder(1)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public MusicOverrides Overrides { get; set; }
    
    [FieldOrder(2)]
    public uint BankSourceCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(BankSourceCount))]
    public List<BankSourceData> BankSourceData { get; set; } = new();
    
    // TODO: On v156 and above, the overrides are here and not earlier
    
    [FieldOrder(4)]
    public uint NumPlaylistItem { get; set; }
    
    [FieldOrder(5)]
    [FieldCount(nameof(NumPlaylistItem))]
    public List<TrackSourceInfo> Playlist { get; set; } = new();
    
    [FieldOrder(6)]
    [SerializeWhen(nameof(NumPlaylistItem), 0, ComparisonOperator.GreaterThan)]
    public uint NumSubTracks { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhenVersion(62, ComparisonOperator.GreaterThan)]
    public uint NumClipAutomationItems { get; set; }
    
    [FieldOrder(8)]
    [SerializeWhenVersion(62, ComparisonOperator.GreaterThan)]
    [FieldCount(nameof(NumClipAutomationItems))]
    public List<ClipAutomationItem> ClipAutomationItems { get; set; } = [];
    
    [FieldOrder(9)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();
    
    [FieldOrder(10)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short Loop { get; set; }
    
    [FieldOrder(11)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short LoopModMin { get; set; }
    
    [FieldOrder(12)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short LoopModMax { get; set; }

    [FieldOrder(13)] 
    public MusicTrackType TrackType { get; set; } = new();
    
    [FieldOrder(14)]
    [SerializeWhen(nameof(TrackType.Value), MusicTrackType.MusicTrackTypeInner.Switch)]
    public TrackSwitchParams SwitchParams { get; set; } = new();
    
    [FieldOrder(15)]
    [SerializeWhen(nameof(TrackType.Value), MusicTrackType.MusicTrackTypeInner.Switch)]
    public TrackTransitionParams TransitionParams { get; set; } = new();
    
    
    [FieldOrder(16)]
    public int LookAheadTime { get; set; }
    
    // TODO: Playlist is serialized down here when version <= 26
    
    [FieldOrder(17)]
    [SerializeWhenVersion(26, ComparisonOperator.LessThanOrEqual)]
    public uint Unknown1 { get; set; }
}

public class TrackSourceInfo
{
    [FieldOrder(0)]
    public uint TrackId { get; set; }
    
    [FieldOrder(1)]
    public uint SourceId { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(150, ComparisonOperator.GreaterThan)]
    public uint CacheId { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(132, ComparisonOperator.GreaterThan)]
    public uint EventId { get; set; }
    
    [FieldOrder(4)]
    public double PlayAt { get; set; }
    
    [FieldOrder(5)]
    public double BeginTrimOffset { get; set; }
    
    [FieldOrder(6)]
    public double EndTrimOffset { get; set; }
    
    [FieldOrder(7)]
    public double SrcDuration { get; set; }
}

public class ClipAutomationItem
{
    [FieldOrder(0)]
    public uint ClipIndex { get; set; }

    [FieldOrder(1)] 
    public ClipAutomationType ClipAutomationType { get; set; } = new();
    
    [FieldOrder(2)]
    public uint NumPoints { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(NumPoints))]
    public List<RtpcGraphItem> ArrayGraphPoints { get; set; } = new();
}

public class TrackSwitchParams
{
    [FieldOrder(1)] 
    public GroupType GroupType { get; set; } = new();

    [FieldOrder(2)]
    public uint GroupId { get; set; }

    [FieldOrder(3)]
    public uint DefaultSwitchId { get; set; }
    
    [FieldOrder(4)]
    public uint SwitchAssocCount { get; set; }
    
    [FieldOrder(5)]
    [FieldCount(nameof(SwitchAssocCount))]
    public List<uint> SwitchAssocIds { get; set; } = new();
}

public class TrackTransitionParams
{
    [FieldOrder(0)]
    public MusicFade SourceFade { get; set; } = new();
    
    [FieldOrder(1)]
    public SyncType SyncType { get; set; }
    
    [FieldOrder(2)]
    public uint CueFilterHash { get; set; }
    
    [FieldOrder(3)]
    public MusicFade DestinationFade { get; set; } = new();
}

public enum SyncType : uint
{
    Immediate,
    NextGrid,
    NextBar,
    NextBeat,
    NextMarker,
    NextUserMarker,
    EntryMarker,
    ExitMarker,
    ExitNever, // v88 > only
    LastExitPosition //v88 > only
}

public class MusicFade
{
    [FieldOrder(0)]
    public int TransitionTime { get; set; }
    
    [FieldOrder(1)]
    public CurveInterpolation FadeCurve { get; set; }
    
    [FieldOrder(2)]
    public int FadeOffset { get; set; }
}