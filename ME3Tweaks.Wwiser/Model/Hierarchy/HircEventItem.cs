using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircEventItem : HircItem
{
    [FieldOrder(0)]
    public BadVarCount ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount($"{nameof(ActionCount)}.{nameof(ActionCount.Value)}")]
    public required List<uint> ActionIds { get; set; }
}

