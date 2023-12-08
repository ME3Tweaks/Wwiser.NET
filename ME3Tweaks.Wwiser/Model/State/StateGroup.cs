using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateGroup
{
    [FieldOrder(1)]
    public SyncType StateSyncType { get; set; }

    [FieldOrder(2)]
    public StateCount StateCount { get; set; } = new();

    [FieldOrder(3)]
    [FieldCount($"{nameof(Model.State.StateCount)}.{nameof(Model.State.StateCount.Value)}")]
    public List<State> States { get; set; } = new();
}