using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetSwitch : IActionParams
{
    [FieldOrder(1)]
    public uint SwitchGroupId { get; set; }
    
    [FieldOrder(2)]
    public uint SwitchStateId { get; set; }
}