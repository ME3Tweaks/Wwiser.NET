using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetFX : IActionParams
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsAudioDeviceElement { get; set; }
    
    [FieldOrder(3)]
    public byte SlotIndex { get; set; }
    
    [FieldOrder(4)]
    public uint FXId { get; set; }
    
    [FieldOrder(5)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsShared { get; set; }
    
    [FieldOrder(6)]
    public ExceptParams ExceptParams { get; set; } = new();
}