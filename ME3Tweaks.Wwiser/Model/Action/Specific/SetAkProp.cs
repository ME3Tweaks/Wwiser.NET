using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class SetAkProp : ISpecificParams
{
    [FieldOrder(0)] 
    public SmartValueMeaning ValueMeaning { get; set; } = new();
    
    [FieldOrder(1)] 
    public RangedParameterFloat RandomizerModifier { get; set; } = new();
    
    [FieldOrder(2)]
    [SerializeWhenVersion(26, ComparisonOperator.LessThanOrEqual)]
    public float Unk1 { get; set; }
}