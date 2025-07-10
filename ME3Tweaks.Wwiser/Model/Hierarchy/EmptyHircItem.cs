using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class EmptyHircItem : HircItem
{
    [FieldOrder(0)]
    [FieldLength(nameof(HircItemContainer.Size), AncestorType = typeof(HircItemContainer),
        RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public byte[] Data { get; set; } = Array.Empty<byte>();

    [Ignore] public override HircType HircType => HircType.Action;
}