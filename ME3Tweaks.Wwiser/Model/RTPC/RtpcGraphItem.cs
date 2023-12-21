using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class RtpcGraphItem
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.Float4)]
    public float From { get; set; }
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.Float4)]
    public float To { get; set; }
    
    [FieldOrder(3)]
    public CurveInterpolation Interp { get; set; }

    public RtpcGraphItem Clone()
    {
        return (RtpcGraphItem)MemberwiseClone();
    }
}