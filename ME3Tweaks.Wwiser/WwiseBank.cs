using ME3Tweaks.Wwiser.Model;
// ReSharper disable InconsistentNaming

namespace ME3Tweaks.Wwiser;

public class WwiseBank
{
    public BankHeaderChunk BKHD { get; set; } = new();
    
    public MediaIndexChunk? DIDX { get; set; }
    
    public DataChunk? DATA { get; set; }
    
    public HierarchyChunk? HIRC { get; set; }
    
    public StringMappingChunk? STID { get; set; }
    
    public PlatformChunk? PLAT { get; set; }
    
    public PluginChunk? INIT { get; set; }

    public WwiseBank(WwiseBankRoot root)
    {
        foreach (var chunk in root.Chunks)
        {
            switch (chunk.Tag)
            {
                case "BKHD":
                    BKHD = (chunk.Chunk as BankHeaderChunk)!;
                    break;
                case "DIDX":
                    DIDX = chunk.Chunk as MediaIndexChunk;
                    break;
                case "DATA":
                    DATA = chunk.Chunk as DataChunk;
                    break;
                case "HIRC":
                    HIRC = chunk.Chunk as HierarchyChunk;
                    break;
                case "STID":
                    STID = chunk.Chunk as StringMappingChunk;
                    break;
                case "PLAT":
                    PLAT = chunk.Chunk as PlatformChunk;
                    break;
                case "INIT":
                    INIT = chunk.Chunk as PluginChunk;
                    break;
                
            }
        }
    }

    public WwiseBankRoot ToModel()
    {
        var chunks = new List<Chunk?> 
            {
                BKHD, DIDX, DATA, HIRC, STID, PLAT, INIT
            }.Where(x => x != null)
            .Where(x => Chunk.IsAllowedInVersion(x!, BKHD.BankGeneratorVersion))
            .Select(x => new ChunkContainer(x!))
            .ToArray();

        return new WwiseBankRoot()
        {
            Chunks = chunks
        };
    }
}