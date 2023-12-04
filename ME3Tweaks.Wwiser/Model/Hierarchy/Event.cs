using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Event : HircItem
{
    [FieldOrder(0)] 
    public VarCount ActionCount { get; set; } = new();

    [FieldOrder(1)]
    [FieldCount($"{nameof(ActionCount)}.{nameof(ActionCount.Value)}")]
    public required List<uint> ActionIds { get; set; } = new();
}

