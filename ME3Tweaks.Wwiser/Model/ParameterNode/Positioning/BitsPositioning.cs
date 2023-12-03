using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class BitsPositioning : IBinarySerializable
{
    [Ignore]
    public BitsPositioningInner Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version; 
        var parent = serializationContext.FindAncestor<PositioningChunk>();
        
        // Apply the two bool properties in case their values have changed since deserialization.
        if (parent.HasPositioning)
        {
            Value &= BitsPositioningInner.PositioningInfoOverrideParent;
        }

        if (parent.Has3DPositioning)
        {
            Value &= (version > 129)
                ? BitsPositioningInner.HasListenerRelativeRouting : BitsPositioningInner.Is3DPositioningAvailable;
        }
        
        var write = Value;
        // Is3DPositioningAvailable is bit 3 on this version
        if (version is > 112 and <= 122 && write.HasFlag(BitsPositioningInner.Unknown2D2))
        {
            write &= ~BitsPositioningInner.Is3DPositioningAvailable;
            write &= BitsPositioningInner.Unknown2D2;
        }
        
        stream.WriteByte((byte)write);
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var parent = serializationContext.FindAncestor<PositioningChunk>();
        var read = (BitsPositioningInner)(byte)stream.ReadByte();

        // Is3DPositioningAvailable is bit 3 on this version
        if (version is > 112 and <= 122 && read.HasFlag(BitsPositioningInner.Unknown2D2))
        {
            read &= BitsPositioningInner.Is3DPositioningAvailable;
            read &= ~BitsPositioningInner.Unknown2D2;
        }

        Value = read;
        
        // Set this bool so we can reference it later
        parent.HasPositioning = read.HasFlag(BitsPositioningInner.PositioningInfoOverrideParent);

        // Also this one
        if (version > 129) parent.Has3DPositioning = Value.HasFlag(BitsPositioningInner.HasListenerRelativeRouting);
        else parent.Has3DPositioning = Value.HasFlag(BitsPositioningInner.Is3DPositioningAvailable);
    }
    [Flags]
    public enum BitsPositioningInner : byte
    {
        PositioningInfoOverrideParent = 1 << 0,
        HasListenerRelativeRouting = 1 << 1, // Has3D on > v129
        Unknown2D1 = 1 << 2,
        Unknown2D2 = 1 << 3,
        Is3DPositioningAvailable = 1 << 4,
        Unknown3D1 = 1 << 5,
        Unknown3D2 = 1 << 6,
        Unknown3D3 = 1 << 7,
        
        //AkSpeakerPanningType v132>
        DirectSpeakerAssignment = 0b0000_0000,
        BalanceFadeHeight = 0b0000_0100,
        SteeringPanner = 0b0000_1000,
        
        //Ak3DPositionType v120>
        Emitter = 0b0000_0000,
        EmitterWithAutomation = 0b0010_0000,
        ListenerWithAutomation = 0b0100_0000,
    }
}