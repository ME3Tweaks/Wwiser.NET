using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class UnionRange(Uni low, Uni high)
{
    [FieldOrder(0)]
    public Uni Low { get; set; } = low;

    [FieldOrder(1)]
    public Uni High { get; set; } = high;

    public UnionRange() : this(new Uni(0f), new Uni(0f)) { }
}