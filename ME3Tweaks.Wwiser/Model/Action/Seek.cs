using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Seek
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsSeekRelativeToDuration{ get; set; }

    [FieldOrder(3)] 
    public RangedParameterFloat SeekValue { get; set; } = new();
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    public bool SnapToNearestMarker{ get; set; }
    
    [FieldOrder(5)]
    public ExceptParams ExceptParams { get; set; } = new();
}