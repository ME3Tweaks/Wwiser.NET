using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateChunk_Aware : IStateChunk
{
    [FieldOrder(0)]
    public VarCount StatePropsCount { get; set; } = new();
    
    [FieldOrder(1)]
    [FieldCount($"{nameof(StatePropsCount)}.{nameof(StatePropsCount.Value)}")]
    public List<StateProp> PropertyInfo { get; set; } = new();
    
    [FieldOrder(2)]
    public VarCount StateGroupsCount { get; set; } = new();
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; } = new();
}