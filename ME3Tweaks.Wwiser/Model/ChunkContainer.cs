using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model
{
    public class ChunkContainer(Chunk chunk)
    {
        [FieldOrder(0)]
        [FieldLength(4)]
        public string Tag { get; init; } = chunk.Tag;

        [FieldOrder(1)]
        public uint ChunkSize { get; set; }

        [FieldOrder(2)]
        [FieldLength(nameof(ChunkSize))]
        [Subtype(nameof(Tag), "BKHD", typeof(BankHeaderChunk))] // > 26
        [Subtype(nameof(Tag), "HIRC", typeof(HierarchyChunk))]
        [Subtype(nameof(Tag), "DATA", typeof(DataChunk))]
        //[Subtype(nameof(Tag), "FXPR", typeof(FakeChunk))]
        [Subtype(nameof(Tag), "ENVS", typeof(EnvironmentSettingsChunk))]
        [Subtype(nameof(Tag), "STID", typeof(StringMappingChunk))] // > v26
        [Subtype(nameof(Tag), "STMG", typeof(GlobalSettingsChunk))] // >= v14
        [Subtype(nameof(Tag), "DIDX", typeof(MediaIndexChunk))] // >= v34
        [Subtype(nameof(Tag), "PLAT", typeof(PlatformChunk))] // >= v113
        [Subtype(nameof(Tag), "INIT", typeof(PluginChunk))] // >= v118
        public Chunk Chunk { get; set; } = chunk;
    }
}
