using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.GlobalSettings;

public class STMGReverberator : AkIdentifiable
{
    [FieldOrder(1)]
    public float Time { get; set; }
    
    [FieldOrder(2)]
    public float HFRatio { get; set; }
    
    [FieldOrder(3)]
    public float DryLevel { get; set; }
    
    [FieldOrder(4)]
    public float WetLevel { get; set; }
    
    [FieldOrder(5)]
    public float Spread { get; set; }
    
    [FieldOrder(6)]
    public float Density { get; set; }
    
    [FieldOrder(7)] 
    public float Quality { get; set; }
    
    [FieldOrder(8)] 
    public float Diffusion { get; set; }
    
    [FieldOrder(9)] 
    public float Scale { get; set; }
}