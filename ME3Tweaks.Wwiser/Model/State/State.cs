using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class State : AkIdentifiable
{
    // Lower versions - reference to something else?
    [SerializeWhenVersion(145, ComparisonOperator.LessThanOrEqual)]
    public uint StateInstanceId { get; set; }
    
    // Higher versions, data is inlined???? idk
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public PropBundle<ushort, float> Properties { get; set; } = new();
    
}