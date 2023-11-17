using BinarySerialization;

namespace Wwiser.NET.Model
{
    public abstract class Chunk
    {
        [Ignore]
        public abstract string Tag { get; }

        public ChunkContainer ToContainer()
        {
            return new ChunkContainer() { 
                Chunk = this, 
                Tag = Tag 
            };
        }
    }

    public class FakeChunk : Chunk
    {
        public override string Tag => "FAKE";

        [FieldOrder(0)]
        [ItemLength(nameof(ChunkContainer.ChunkSize), RelativeSourceMode = RelativeSourceMode.FindAncestor, AncestorType = typeof(ChunkContainer))]
        byte[]? data;

    }
}
