using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircEventItem : HircItem
{
    [FieldOrder(0)]
    public uint ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ActionCount))]
    public required List<uint> ActionIds { get; set; } = new();
}