using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class EmptyHircItem : HircItem
{
    [FieldOrder(0)]
    public byte[] Data { get; set; } = [];

    [Ignore] public override HircType HircType => HircType.None;
}