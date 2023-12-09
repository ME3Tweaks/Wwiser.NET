using System.Collections;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Children : IEnumerable<uint>
{
    [FieldOrder(1)]
    public uint ChildrenCount { get; set; }

    [FieldOrder(2)]
    [FieldCount(nameof(ChildrenCount))]
    public List<uint> ChildrenValues { get; set; } = new();

    [Ignore]
    public uint this[int i]
    {
        get => ChildrenValues[i];
        set => ChildrenValues[i] = value;
    }

    public IEnumerator<uint> GetEnumerator()
    {
        return ChildrenValues.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}