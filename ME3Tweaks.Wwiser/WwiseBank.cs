using ME3Tweaks.Wwiser.Model;
// ReSharper disable InconsistentNaming

namespace ME3Tweaks.Wwiser;

public class WwiseBank
{
    public BankHeaderChunk BKHD { get; set; } = new();
    
    public List<EmbeddedFile> EmbeddedFiles { get; set; } = new();
    
    public HierarchyChunk? HIRC { get; set; }
    
    public FxParamChunk? FXPR { get; set; }
    
    public StringMappingChunk? STID { get; set; }
    
    public GlobalSettingsChunk? STMG { get; set; }
    
    public PlatformChunk? PLAT { get; set; }
    
    public EnvironmentSettings? ENVS { get; set; }
    
    public PluginChunk? INIT { get; set; }
}