using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class WwiseBankRoot
{
    [FieldOrder(0)]
    public required ChunkContainer[] Chunks { get; set; }
}