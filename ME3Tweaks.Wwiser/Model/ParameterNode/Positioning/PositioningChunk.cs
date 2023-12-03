using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;
// This class has a lot of logic based on prior data,
// so basically all the serialized properties use custom serialization
// and depend on the [Ignore] bools in this class

public class PositioningChunk
{
    [Ignore]
    public bool HasPositioning { get; set; }
    
    [Ignore]
    public bool Has3DPositioning { get; set; }
    
    [Ignore]
    public bool Has2DPositioning { get; set; }
    
    [Ignore]
    public bool HasPanner { get; set; }
    
    [FieldOrder(0)]
    public BitsPositioning PositioningBits { get; set; }

    [FieldOrder(1)] 
    public PositioningFlags PositioningFlags { get; set; }

    [FieldOrder(2)]
    [SerializeWhen(nameof(Has3DPositioning), true)]
    public Gen3DParams Gen3DParams { get; set; }
    
}