using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetRTPC : IActionParams
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(2)]
    public uint RTPCId { get; set; }
    
    [FieldOrder(3)]
    public float RTPCValue { get; set; }
}