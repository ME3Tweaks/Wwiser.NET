using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class MediaIndexChunk : Chunk
{
    public override string Tag => @"DIDX";
    
    [FieldOrder(0)]
    [FieldLength(nameof(ChunkContainer.ChunkSize), 
        RelativeSourceMode = RelativeSourceMode.FindAncestor,
        AncestorLevel = 2)]
    public List<MediaHeader> LoadedMedia { get; set; }
}

public class MediaHeader
{
    [FieldOrder(0)]
    public uint Id { get; set; }
    
    [FieldOrder(1)]
    public uint Offset { get; set; }
    
    [FieldOrder(2)]
    public uint Size { get; set; }
}