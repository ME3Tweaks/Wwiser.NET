using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainerV128 : IHircItemContainer
{
    [FieldOrder(0)]
    public required HircType128 Type { get; set; }
    
    [FieldOrder(2)]
    public uint Size { get; set; }

    [FieldOrder(3)]
    [FieldLength(nameof(Size))]
    [SubtypeFactory(nameof(Type), typeof(HircTypeFactory128))]
    public required HircItem Item { get; set; }
}