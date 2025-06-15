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
    
    public GlobalSettingsChunk? STMG { get; set; }
    
    public PlatformChunk? PLAT { get; set; }
    
    public PluginChunk? INIT { get; set; }

    public List<EmbeddedFile> GetEmbeddedFiles()
    {
        if(DATA is null || DIDX is null) throw new InvalidOperationException("DATA or DIDX chunk is not initialized.");
        var embeddedFiles = new List<EmbeddedFile>();
        var dataStream = new MemoryStream(DATA.Data);
        foreach (var media in DIDX.LoadedMedia.OrderBy(m => m.Offset))
        {
            var embeddedFile = new EmbeddedFile
            {
                Id = media.Id,
                Data = new byte[media.Size]
            };
            dataStream.Seek(media.Offset, SeekOrigin.Begin);
            dataStream.ReadExactly(embeddedFile.Data, 0, (int)media.Size);
            embeddedFiles.Add(embeddedFile);
        }
        return embeddedFiles;
    }
    
    public void AddOrReplaceEmbeddedFile(EmbeddedFile file)
    {
        throw new NotImplementedException();
    }
}