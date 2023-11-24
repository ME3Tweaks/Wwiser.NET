using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircEventItem : HircItem, IHircEventItem
{
    [FieldOrder(0)]
    public uint ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ActionCount))]
    public required List<uint> ActionIds { get; set; }
}

public class HircEventItem122 : HircItem, IHircEventItem
{
    //TODO: Fix this, find some way to merge with prev class.
    //Cheap hack to deal with potentially variable length count
    [FieldOrder(0)]
    public byte ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ActionCount))]
    public required List<uint> ActionIds { get; set; }
}

public interface IHircEventItem
{
    public List<uint> ActionIds { get; set; }
}