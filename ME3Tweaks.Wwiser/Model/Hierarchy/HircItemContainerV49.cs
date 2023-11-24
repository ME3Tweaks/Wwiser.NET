using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainerV49 : IHircItemContainer
{
    [FieldOrder(0)]
    [SerializeAs(SerializedType.UInt8)]
    public required HircType Type { get; set; }
    
    [FieldOrder(1)]
    public uint Size { get; set; }

    [FieldOrder(2)]
    [FieldLength(nameof(Size))]
    [SubtypeFactory(nameof(Type), typeof(HircTypeFactory))]
    public required HircItem Item { get; set; }
}