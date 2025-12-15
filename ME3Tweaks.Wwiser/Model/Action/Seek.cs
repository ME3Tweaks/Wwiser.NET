using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class Seek : IActionParams
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsSeekRelativeToDuration{ get; set; }

    [FieldOrder(2)] 
    public RangedParameterFloat SeekValue { get; set; } = new();
    
    [FieldOrder(3)]
    [SerializeAs(SerializedType.UInt1)]
    public bool SnapToNearestMarker{ get; set; }
    
    [FieldOrder(4)]
    public ExceptParams ExceptParams { get; set; } = new();
}