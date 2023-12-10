using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

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
    public float SeekValue { get; set; }
    
    [FieldOrder(4)]
    public float SeekValueMin { get; set; }
    
    [FieldOrder(5)]
    public float SeekValueMax { get; set; }
    
    [FieldOrder(6)]
    [SerializeAs(SerializedType.UInt1)]
    public bool SnapToNearestMarker{ get; set; }
    
    [FieldOrder(7)]
    public ExceptParams ExceptParams { get; set; } = new();
}