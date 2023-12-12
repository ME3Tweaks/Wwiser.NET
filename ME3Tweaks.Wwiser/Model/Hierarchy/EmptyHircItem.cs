using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class EmptyHircItem : HircItem
{
    [FieldOrder(0)]
    [FieldLength(nameof(HircItemContainer.Size), AncestorType = typeof(HircItemContainer),
        RelativeSourceMode = RelativeSourceMode.FindAncestor)]
    public byte[] Data { get; set; } = Array.Empty<byte>();
}