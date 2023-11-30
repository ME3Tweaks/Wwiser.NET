using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateChunk_Aware : IStateChunk
{
    [FieldOrder(0)]
    public VarCount StatePropsCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount($"{nameof(StatePropsCount)}.{nameof(StatePropsCount.Value)}")]
    public List<StateProp> PropertyInfo { get; set; }
    
    [FieldOrder(2)]
    public VarCount StateGroupsCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; }
}