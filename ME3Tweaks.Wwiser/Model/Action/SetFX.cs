using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class SetFX : IActionParams
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsAudioDeviceElement { get; set; }
    
    [FieldOrder(2)]
    public byte SlotIndex { get; set; }
    
    [FieldOrder(3)]
    public uint FXId { get; set; }
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsShared { get; set; }
    
    [FieldOrder(5)]
    public ExceptParams ExceptParams { get; set; } = new();
}