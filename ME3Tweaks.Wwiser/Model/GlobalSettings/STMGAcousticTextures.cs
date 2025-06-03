using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.GlobalSettings;

public class STMGAcousticTextures
{
    [FieldOrder(1)]
    public uint TextureCount { get; set; }
    
    [FieldOrder(2)]
    [FieldCount(nameof(TextureCount))]
    [SerializeWhenVersion(122, ComparisonOperator.LessThanOrEqual)]
    public List<AcousticTextureV122> TexturesV122 { get; set; } = new();
    
    [FieldOrder(3)]
    [FieldCount(nameof(TextureCount))]
    [SerializeWhenVersion(122, ComparisonOperator.GreaterThan)]
    public List<AcousticTextureV126> TexturesV126 { get; set; } = new();
}

public abstract class AcousticTexture : AkIdentifiable;

public class AcousticTextureV122 : AcousticTexture
{
    [FieldOrder(1)]
    public ushort OnOffBand1 { get; set; }
    
    [FieldOrder(2)]
    public ushort OnOffBand2 { get; set; }
    
    [FieldOrder(3)]
    public ushort OnOffBand3 { get; set; }
    
    [FieldOrder(4)]
    public ushort FilterTypeBand1 { get; set; }
    
    [FieldOrder(5)]
    public ushort FilterTypeBand2 { get; set; }
    
    [FieldOrder(6)]
    public ushort FilterTypeBand3 { get; set; }
    
    [FieldOrder(7)]
    public float FrequencyBand1 { get; set; }
    
    [FieldOrder(8)]
    public float FrequencyBand2 { get; set; }
    
    [FieldOrder(9)]
    public float FrequencyBand3 { get; set; }
    
    [FieldOrder(10)]
    public float QFactorBand1 { get; set; }
    
    [FieldOrder(11)]
    public float QFactorBand2 { get; set; }
    
    [FieldOrder(12)]
    public float QFactorBand3 { get; set; }
    
    [FieldOrder(13)]
    public float GainBand1 { get; set; }
    
    [FieldOrder(14)]
    public float GainBand2 { get; set; }
    
    [FieldOrder(15)]
    public float GainBand3 { get; set; }
    
    [FieldOrder(16)]
    public float OutputGain { get; set; }
}

public class AcousticTextureV126 : AcousticTexture
{
    [FieldOrder(1)]
    public float AbsorptionOffset { get; set; }
    
    [FieldOrder(2)]
    public float AbsorptionLow { get; set; }
    
    [FieldOrder(3)]
    public float AbsorptionMidLow { get; set; }
    
    [FieldOrder(4)]
    public float AbsorptionMidHigh { get; set; }
    
    [FieldOrder(5)]
    public float AbsorptionHigh { get; set; }
    
    [FieldOrder(6)]
    public float Scattering { get; set; }
}