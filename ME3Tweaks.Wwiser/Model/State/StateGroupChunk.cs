using System.Collections.Generic;
using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateGroupChunk : AkIdentifiable
{
    [FieldOrder(1)]
    public SyncType StateSyncType { get; set; }
    
    [FieldOrder(2)]
    public VarCount StateCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateCount)}.{nameof(StateCount.Value)}")]
    public List<State> States { get; set; }

    public enum SyncType : byte
    {
        Immediate,
        NextGrid,
        NextBar,
        NextBeat,
        NextMarker,
        NextUserMarker,
        EntryMarker,
        ExitMarker,
        ExitNever,
        LastExitPosition
    }
}