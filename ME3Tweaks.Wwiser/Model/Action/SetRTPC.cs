using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetRTPC : IActionParams
{
    [FieldOrder(1)]
    public uint RTPCId { get; set; }
    
    [FieldOrder(2)]
    public float RTPCValue { get; set; }
}