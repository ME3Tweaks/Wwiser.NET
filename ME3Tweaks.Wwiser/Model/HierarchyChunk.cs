using System.Runtime.CompilerServices;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model;

public class HierarchyChunk : Chunk
{
    public override string Tag => "HIRC";
    
    [FieldOrder(0)]
    public uint ItemCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount(nameof(ItemCount))]
    [ItemSubtypeFactory(nameof(BankSerializationContext.Version), typeof(HircContainerTypeFactory), RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public required List<IHircItemContainer> Items { get; set; }

}