using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetState : IActionParams
{
    [FieldOrder(1)]
    public uint StateGroupId { get; set; }
    
    [FieldOrder(2)]
    public uint TargetStateId { get; set; }
}