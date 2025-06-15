using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.State;

// Covers StateChunk and StateChunk_Aware?
public class StateChunk
{
    [FieldOrder(0)]
    [SerializeWhenVersion(125, ComparisonOperator.GreaterThan)]
    public VarCount StatePropsCount { get; set; } = new();
    
    [FieldOrder(1)]
    [FieldCount($"{nameof(StatePropsCount)}.{nameof(StatePropsCount.Value)}")]
    [SerializeWhenVersion(125, ComparisonOperator.GreaterThan)]
    public List<StateProp> PropertyInfo { get; set; } = new();
    
    [FieldOrder(2)]
    public VarCount StateGroupsCount { get; set; } = new();
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; } = new();
}