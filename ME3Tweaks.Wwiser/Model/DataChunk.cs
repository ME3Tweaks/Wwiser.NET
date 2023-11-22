using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class DataChunk : Chunk
{
    public override string Tag => @"DATA";
    
    [FieldOrder(0)]
    [FieldLength(nameof(ChunkContainer.ChunkSize), 
        RelativeSourceMode = RelativeSourceMode.FindAncestor,
        AncestorLevel = 2)]
    public required byte[] Data { get; set; }
}