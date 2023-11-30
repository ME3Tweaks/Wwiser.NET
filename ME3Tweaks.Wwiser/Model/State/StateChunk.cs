using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateChunk : IStateChunk
{
    [FieldOrder(0)]
    public VarCount StateGroupsCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; }
}