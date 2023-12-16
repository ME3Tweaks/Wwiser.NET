using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class RtpcParameterNodeBase
{
    [FieldOrder(0)] 
    public V36ShortCount RTPCCount { get; set; } = new();

    [FieldOrder(1)]
    [FieldCount($"{nameof(RTPCCount)}.{nameof(RTPCCount.Value)}")]
    public List<Rtpc> Rtpcs { get; set; } = new();
}