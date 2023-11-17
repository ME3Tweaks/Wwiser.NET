using BinarySerialization;

namespace Wwiser.NET.Model
{
    public class ChunkContainer
    {
        [FieldOrder(0)]
        [FieldLength(4)]
        public string Tag { get; set; }

        [FieldOrder(1)]
        public uint ChunkSize { get; set; }

        [FieldOrder(2)]
        [FieldLength(nameof(ChunkSize))]
        [Subtype(nameof(Tag), "BKHD", typeof(BankHeader))]
        //[Subtype(nameof(Tag), "HIRC", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "DATA", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "FXPR", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "ENVS", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "STID", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "STMG", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "DIDX", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "PLAT", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "INIT", typeof(FakeChunk))]
        public Chunk Chunk { get; set; }
    }
}
