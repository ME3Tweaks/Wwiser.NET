using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public interface IAkIdentifiable
{
    public uint Id { get; set; }
}

public class AkIdentifiable : IAkIdentifiable
{
    [FieldOrder(0)]
    public uint Id { get; set; }
}