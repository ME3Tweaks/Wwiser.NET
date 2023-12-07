using BinarySerialization;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class RtpcParameterNodeBase
{
    //TODO: <=v36 this is a uint - not relevant to mass effect
    [FieldOrder(0)] 
    public ushort RtpcCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount(nameof(RtpcCount))]
    public List<Rtpc> Rtpcs { get; set; } = new();
}