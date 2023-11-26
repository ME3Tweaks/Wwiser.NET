using System.Collections.Generic;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Event : HircItem
{
    [FieldOrder(0)]
    public VarCount ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount($"{nameof(ActionCount)}.{nameof(ActionCount.Value)}")]
    public required List<uint> ActionIds { get; set; }
}

