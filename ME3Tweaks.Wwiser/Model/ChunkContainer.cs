﻿using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model
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
        [Subtype(nameof(Tag), "BKHD", typeof(BankHeaderChunk))]
        //[Subtype(nameof(Tag), "HIRC", typeof(FakeChunk))]
        [Subtype(nameof(Tag), "DATA", typeof(DataChunk))]
        //[Subtype(nameof(Tag), "FXPR", typeof(FakeChunk))]
        //[Subtype(nameof(Tag), "ENVS", typeof(FakeChunk))]
        [Subtype(nameof(Tag), "STID", typeof(StringMappingChunk))]
        //[Subtype(nameof(Tag), "STMG", typeof(FakeChunk))]
        [Subtype(nameof(Tag), "DIDX", typeof(MediaIndexChunk))]
        [Subtype(nameof(Tag), "PLAT", typeof(PlatformChunk))]
        [Subtype(nameof(Tag), "INIT", typeof(PluginChunk))]
        public Chunk Chunk { get; set; }
    }
}