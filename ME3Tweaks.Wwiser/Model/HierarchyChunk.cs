using System.Collections.Generic;
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
    public required List<HircItemContainer> Items { get; set; } = new();

}