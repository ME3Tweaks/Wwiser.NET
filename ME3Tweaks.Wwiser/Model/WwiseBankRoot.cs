using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class WwiseBankRoot
{
    [FieldOrder(0)]
    public required ChunkContainer[] Chunks { get; set; }
    
    public static WwiseBankRoot FromBank(WwiseBank bank)
    {
        var chunks = new List<Chunk?> 
            {
                bank.BKHD, bank.DIDX, bank.DATA, bank.HIRC, bank.STID, bank.STMG, bank.PLAT, bank.INIT
            }.Where(x => x is not null)
            .Select(x => x!)
            .Where(x => x.IsAllowedInVersion(bank.BKHD.BankGeneratorVersion))
            .Select(x => new ChunkContainer(x))
            .ToArray();

        return new WwiseBankRoot()
        {
            Chunks = chunks
        };
    }

    public WwiseBank ToBank()
    {
        WwiseBank bank = new();
        foreach (var chunk in Chunks)
        {
            switch (chunk.Tag)
            {
                case "BKHD":
                    bank.BKHD = (chunk.Chunk as BankHeaderChunk)!;
                    break;
                case "DIDX":
                    bank.DIDX = chunk.Chunk as MediaIndexChunk;
                    break;
                case "DATA":
                    bank.DATA = chunk.Chunk as DataChunk;
                    break;
                case "HIRC":
                    bank.HIRC = chunk.Chunk as HierarchyChunk;
                    break;
                case "STID":
                    bank.STID = chunk.Chunk as StringMappingChunk;
                    break;
                case "STMG":
                    bank.STMG = chunk.Chunk as GlobalSettingsChunk;
                    break;
                case "PLAT":
                    bank.PLAT = chunk.Chunk as PlatformChunk;
                    break;
                case "INIT":
                    bank.INIT = chunk.Chunk as PluginChunk;
                    break;
            }
        }

        return bank;
    }
}