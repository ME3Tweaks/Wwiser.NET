using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class Gen3DParams
{
    [Ignore]
    public PositioningType Type { get; set; }
    
    [Ignore]
    public SpatializationMode Mode { get; set; }

    [Ignore]
    public uint AttenuationId { get; set; }
    
    [Ignore]
    public bool IsSpatialized { get; set; }
    
    public void Serialize(Stream stream, PositioningChunk parent, uint version)
    {
        SetPropertiesFromBools(parent, version);
        
        if (version <= 89)
        {
            stream.Write(BitConverter.GetBytes((uint)Type));
        }
        else
        {
            var mode = Mode;
            if (version <= 132)
            {
                // HoldListener and HoldEmitter are one flag lower on version 132 and lower
                if (mode.HasFlag(SpatializationMode.HoldEmitterPosAndOrient))
                {
                    mode &= SpatializationMode.EnableAttenuation;
                    mode &= ~SpatializationMode.HoldEmitterPosAndOrient;
                }
                if (mode.HasFlag(SpatializationMode.HoldListenerOrient))
                {
                    mode &= SpatializationMode.HoldEmitterPosAndOrient;
                    mode &= ~SpatializationMode.HoldListenerOrient;
                }

                // strip flags only found in higher versions
                mode &= ~SpatializationMode.IsNotLooping;
            }
            if(version <= 134)  mode &= ~SpatializationMode.EnableDiffraction;

            stream.WriteByte((byte)mode);
        }
        if (version <= 129) stream.Write(BitConverter.GetBytes(AttenuationId));
        if (version <= 89) stream.WriteBoolByte(IsSpatialized);
    }

    public void Deserialize(Stream stream, PositioningChunk parent, uint version)
    {
        var reader = new BinaryReader(stream);

        if (version <= 89)
        {
            Type = (PositioningType)reader.ReadUInt32();
        }
        else
        {
            var mode = (SpatializationMode)reader.ReadByte();
            if (version <= 132)
            {
                // HoldListener and HoldEmitter are one flag lower on version 132 and lower
                if (mode.HasFlag(SpatializationMode.HoldEmitterPosAndOrient))
                {
                    mode &= SpatializationMode.HoldListenerOrient;
                    mode &= ~SpatializationMode.HoldEmitterPosAndOrient;
                }
                if (mode.HasFlag(SpatializationMode.EnableAttenuation))
                {
                    mode &= SpatializationMode.HoldEmitterPosAndOrient;
                    mode &= ~SpatializationMode.EnableAttenuation;
                }
            }

            Mode = mode;
        }

        if (version <= 129) AttenuationId = reader.ReadUInt32();
        if (version <= 89) IsSpatialized = reader.ReadBoolean();
        
        SetBoolsFromProperties(parent, version);
    }

    /// <summary>
    /// Correctly sets the properties based on version and
    /// the HasAutomation and HasDynamic flags
    /// </summary>
    private void SetPropertiesFromBools(PositioningChunk parent, uint version)
    {
        // TODO: Crossversion - this will need to be implemented
    }

    /// <summary>
    /// Correctly sets the HasAutomation and HasDynamic flags based on the
    /// other properties in the class
    /// </summary>
    private void SetBoolsFromProperties(PositioningChunk parent, uint version)
    {
        // Todo: rewrite so bitwise operators use IsFlag - more readable
        parent.HasAutomation = version switch
        {
            <= 72 => Type is PositioningType.UserDef3D,
            <= 89 => Type is not PositioningType.Positioning2D,
            <= 122 => ((byte)Mode & 3) != 1,
            <= 126 => (((byte)Mode >> 4 ) & 1) != 1,
            <= 129 => (((byte)Mode >> 6 ) & 1) != 1,
            _ => (((byte)Mode >> 5 ) & 3) != 1,
        };

        parent.HasDynamic = version switch
        {
            <= 72 => Type is PositioningType.GameDef3D,
            <= 89 => !parent.HasDynamic,
            _ => false
        };
    }

    public enum PositioningType : uint
    {
        Undefined,
        Positioning2D,
        UserDef3D,
        GameDef3D
    }

    [Flags]
    public enum SpatializationMode : byte
    {
        None = 0b0000_0000,
        PositionOnly = 0b000_0001,
        PositionAndOrientation = 0b0000_0010,
        EnableAttenuation = 1 << 3,
        HoldEmitterPosAndOrient = 1 << 4,
        HoldListenerOrient = 1 << 5,
        EnableDiffraction = 1 << 6,
        IsNotLooping = 1 << 7
    }
}