using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;
// This class has a lot of logic based on prior data,
// so all the serialized properties are dropped out of serialization framework
// and depend on the [Ignore] bools in this class

public class PositioningChunk : IBinarySerializable
{
    [Ignore] 
    public bool HasPositioning { get; set; }
    
    [Ignore]
    public bool Has3DPositioning { get; set; }
    
    [Ignore]
    public bool Has2DPositioning { get; set; }
    
    [Ignore]
    public bool HasPanner { get; set; }
    
    [Ignore]
    public bool HasAutomation { get; set; }
    
    [Ignore]
    public bool HasDynamic { get; set; }
    
    [Ignore]
    public BitsPositioning PositioningBits { get; } = new();

    [Ignore]
    public PositioningFlags PositioningFlags { get; }  = new();

    [Ignore]
    public Gen3DParams Gen3DParams { get; }  = new();

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        PositioningBits.Serialize(stream, this, version);
        PositioningFlags.Serialize(stream, this, version);
        if (Has3DPositioning)
        {
            Gen3DParams.Serialize(stream, this, version);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        PositioningBits.Deserialize(stream, this, version);
        PositioningFlags.Deserialize(stream, this, version);
        if (Has3DPositioning)
        {
            Gen3DParams.Deserialize(stream, this, version);
        }
    }
}