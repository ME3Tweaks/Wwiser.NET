using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class AkIdentifiable
{
    [FieldOrder(0)]
    public uint Id { get; set; }
}