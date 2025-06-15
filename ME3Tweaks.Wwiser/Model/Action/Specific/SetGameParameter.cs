using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class SetGameParameter : ISpecificParams
{
    [FieldOrder(0)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    [SerializeAs(SerializedType.UInt1)]
    public bool BypassTransition { get; set; }

    [FieldOrder(1)] 
    public SmartValueMeaning ValueMeaning { get; set; } = new();

    [FieldOrder(2)] 
    public RangedParameterFloat RandomizerModifier { get; set; } = new();
}