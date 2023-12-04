using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class PropBundle<T1, T2>
{
    [FieldOrder(0)]
    public ushort PropCount { get; set; }

    [FieldOrder(1)]
    [FieldLength(nameof(PropCount))]
    public List<T1> Ids { get; set; } = new();
    
    [FieldOrder(2)]
    [FieldLength(nameof(PropCount))]
    public List<T2> Values { get; set; } = new();
}