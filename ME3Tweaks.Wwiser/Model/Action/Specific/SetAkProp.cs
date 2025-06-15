using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

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