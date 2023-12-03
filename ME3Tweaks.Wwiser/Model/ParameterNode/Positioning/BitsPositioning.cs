using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class BitsPositioning
{
    [Ignore]
    public BitsPositioningInner Value { get; set; }
    
    [Ignore]
    public SpeakerPanningType PanningType { get; set; }
    
    [Ignore]
    public PositionType3D PositionType { get; set; }
    
    public void Serialize(Stream stream, PositioningChunk parent, uint version)
    {
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
        
        stream.WriteByte((byte)((byte)write & ((byte)PanningType << 2) & ((byte)PositionType) << 5));
    }

    public void Deserialize(Stream stream, PositioningChunk parent, uint version)
    {
        var read = (byte)stream.ReadByte();
        var bits = (BitsPositioningInner)read;
        
        // Is3DPositioningAvailable is bit 3 on this version
        if (version is > 112 and <= 122 && bits.HasFlag(BitsPositioningInner.Unknown2D2))
        {
            bits &= BitsPositioningInner.Is3DPositioningAvailable;
            bits &= ~BitsPositioningInner.Unknown2D2;
        }

        Value = bits;
        PanningType = (SpeakerPanningType)(read >> 2);
        PositionType = (PositionType3D)(read >> 5);
        
        // Set this bool so we can reference it later
        parent.HasPositioning = bits.HasFlag(BitsPositioningInner.PositioningInfoOverrideParent);

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
    }

    [Flags]
    public enum SpeakerPanningType : byte
    {
        DirectSpeakerAssignment = 0b0000_0000,
        BalanceFadeHeight = 0b0000_0001,
        SteeringPanner = 0b0000_0010,
    }
    
    [Flags]
    public enum PositionType3D : byte
    {
        Emitter = 0b0000_0000,
        EmitterWithAutomation = 0b0000_0001,
        ListenerWithAutomation = 0b0000_0010,
    }
}