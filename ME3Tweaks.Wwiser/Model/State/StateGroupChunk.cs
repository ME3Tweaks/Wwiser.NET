using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateGroupChunk : AkIdentifiable
{
    [FieldOrder(1)]
    public SyncType StateSyncType { get; set; }
    
    [FieldOrder(2)]
    public StateCount StateCount { get; set; } = new();
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateCount)}.{nameof(StateCount.Value)}")]
    public List<State> States { get; set; } = new();
}