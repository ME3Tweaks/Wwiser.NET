using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class PositioningChunk : IBinarySerializable
{
    [Flags]
    public enum PositioningFlags : byte
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
    public enum PositionType3D : byte
    {
        Emitter = 0b0000_0000,
        EmitterWithAutomation = 0b0000_0001,
        ListenerWithAutomation = 0b0000_0010,
    }

    [Flags]
    public enum SpeakerPanningType : byte
    {
        DirectSpeakerAssignment = 0b0000_0000,
        BalanceFadeHeight = 0b0000_0001,
        SteeringPanner = 0b0000_0010,
    }

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
    public PositioningFlags Flags { get; set; }

    [Ignore]
    public SpeakerPanningType PanningType { get; set; }

    [Ignore]
    public PositionType3D PositionType { get; set; }

    [Ignore]
    public int CenterPct { get; set; }

    [Ignore]
    public float PanRL { get; set; }

    [Ignore]
    public float PanFR { get; set; }

    [Ignore]
    public PositioningType Type { get; set; }

    [Ignore]
    public SpatializationMode Mode { get; set; }

    [Ignore]
    public uint AttenuationId { get; set; }

    [Ignore]
    public bool IsSpatialized { get; set; }

    [Ignore] 
    public Automation AutomationData { get; set; } = new();

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext context)
    {
        var bankContext = context.FindAncestor<BankSerializationContext>();
        var version = bankContext.Version;
        WriteInitialFlags(stream, version);

        if (HasPositioning)
        {
            WritePctPan(stream, version);
        }

        if (Has3DPositioning)
        {
            Write3DParams(stream, version);
            if (HasAutomation)
            {
                var ser = new BinarySerializer();
                ser.Serialize(stream, AutomationData, bankContext);
            }
        }

    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext context)
    {
        var bankContext = context.FindAncestor<BankSerializationContext>();
        var version = bankContext.Version;
        var reader = new BinaryReader(stream);
        ReadInitialFlags(reader, version);

        if (HasPositioning)
        {
            ReadPctPan(version, reader);
        }

        if (Has3DPositioning)
        {
            Read3DParams(stream, version, reader);
            if (HasAutomation)
            {
                var ser = new BinarySerializer();
                AutomationData = ser.Deserialize<Automation>(stream, bankContext);
            }
        }
    }

    private void Write3DParams(Stream stream, uint version)
    {
        if (version <= 89)
        {
            Type = SpatializationHelpers.GetTypeFromBools(HasAutomation, HasDynamic, version);
            stream.Write(BitConverter.GetBytes((uint)Type));
        }
        else
        {
            Mode = SpatializationHelpers.GetModeFromHasAutomation(HasAutomation, Mode, version);
            stream.WriteByte(SpatializationHelpers.GetByteFromMode(Mode, version));
        }

        if (version <= 129) stream.Write(BitConverter.GetBytes(AttenuationId));
        if (version <= 89) stream.WriteBoolByte(IsSpatialized);

        if (HasDynamic) stream.WriteBoolByte(HasDynamic);
    }

    private void WriteInitialFlags(Stream stream, uint version)
    {
        // Apply the two bool properties in case their values have changed since deserialization.
        if (HasPositioning)
        {
            Flags |= PositioningFlags.PositioningInfoOverrideParent;
        }

        if (Has3DPositioning & version > 112)
        {
            Flags |= (version > 129)
                ? PositioningFlags.HasListenerRelativeRouting
                : PositioningFlags.Is3DPositioningAvailable;
        }

        if (HasAutomation && version > 129)
        {
            if (PositionType.HasFlag(PositionType3D.ListenerWithAutomation))
            {
                PositionType &= ~PositionType3D.EmitterWithAutomation;
            }
            else
            {
                PositionType |= PositionType3D.EmitterWithAutomation;
            }
        }

        var write = Flags;
        // Is3DPositioningAvailable is bit 3 on this version
        if (version is > 112 and <= 122 && write.HasFlag(PositioningFlags.Unknown2D2))
        {
            write &= ~PositioningFlags.Is3DPositioningAvailable;
            write |= PositioningFlags.Unknown2D2;
        }

        stream.WriteByte((byte)((byte)write | ((byte)PanningType << 2) | (byte)PositionType << 5));
    }

    private void WritePctPan(Stream stream, uint version)
    {
        if (version <= 56)
        {
            stream.Write(BitConverter.GetBytes(CenterPct));
            stream.Write(BitConverter.GetBytes(PanRL));
            stream.Write(BitConverter.GetBytes(PanFR));
        }

        if (version <= 89)
        {
            if (version > 72) stream.WriteBoolByte(Has2DPositioning);

            stream.WriteBoolByte(Has3DPositioning);
            if ((!Has3DPositioning && version <= 72) || Has2DPositioning)
            {
                stream.WriteBoolByte(HasPanner);
            }
        }
    }

    private void Read3DParams(Stream stream, uint version, BinaryReader reader)
    {
        if (version <= 89)
        {
            Type = (PositioningType)reader.ReadUInt32();

            (HasAutomation, HasDynamic) = SpatializationHelpers.GetBoolFlagsFromType(Type, HasAutomation, version);
        }
        else
        {
            Mode = SpatializationHelpers.GetModeFromByte(reader.ReadByte(), version);
            HasAutomation = SpatializationHelpers.GetHasAutomationFromMode(Mode, HasAutomation, version);
        }

        if (version <= 129) AttenuationId = reader.ReadUInt32();
        if (version <= 89) IsSpatialized = reader.ReadBoolean();


        if (HasDynamic) stream.ReadBoolByte();
    }

    private void ReadInitialFlags(BinaryReader reader, uint version)
    {
        var read = reader.ReadByte();
        var bits = (PositioningFlags)read;

        // Is3DPositioningAvailable is bit 3 on this version
        if (version is > 112 and <= 122 && bits.HasFlag(PositioningFlags.Unknown2D2))
        {
            bits |= PositioningFlags.Is3DPositioningAvailable;
            bits &= ~PositioningFlags.Unknown2D2;
        }

        Flags = bits;
        PanningType = (SpeakerPanningType)(read >> 2);
        PositionType = (PositionType3D)(read >> 5);

        // Set this bool so we can reference it later
        HasPositioning = bits.HasFlag(PositioningFlags.PositioningInfoOverrideParent);

        // Also this one
        if (version > 129)
        {
            Has3DPositioning = Flags.HasFlag(PositioningFlags.HasListenerRelativeRouting);

            // Type == 1 OR Type == 2 and Type != 1
            HasAutomation = PositionType.HasFlag(PositionType3D.EmitterWithAutomation) ||
                            (PositionType.HasFlag(PositionType3D.ListenerWithAutomation) &&
                             !PositionType.HasFlag(PositionType3D.EmitterWithAutomation));
        }
        else Has3DPositioning = Flags.HasFlag(PositioningFlags.Is3DPositioningAvailable);
    }

    private void ReadPctPan(uint version, BinaryReader reader)
    {
        if (version <= 56)
        {
            CenterPct = reader.ReadInt32();
            PanRL = reader.ReadSingle();
            PanFR = reader.ReadSingle();
        }

        if (version <= 89)
        {
            if (version > 72) Has2DPositioning = reader.ReadBoolean();

            Has3DPositioning = reader.ReadBoolean();
            if ((!Has3DPositioning && version <= 72) || Has2DPositioning)
            {
                HasPanner = reader.ReadBoolean();
            }
        }
    }
}