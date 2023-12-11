using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class Stop : ISpecificParams
{
    [FieldOrder(0)]
    public ActiveFlags Flags { get; set; } = new();
}