using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

/// <summary>
/// Wrapper around the HircType enum that serializes it correctly depending on version.
/// Will throw errors when trying to serialize a type that is not compatible with that wwise version.
/// </summary>
public class HircSmartType : IBinarySerializable
{
    public HircType Value { get; set; }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (Value >= HircType.FeedbackBus && context.Version > 126)
        {
            if (Value < HircType.FxShareSet)
            {
                throw new NotSupportedException($"Feedback not supported in version {context.Version}");
            }
            var actualValueToWrite = (uint)Value - 2;
            stream.WriteByte((byte)actualValueToWrite);
        }
        else if (Value is HircType.TimeMod && context.Version <= 126)
        {
            throw new NotSupportedException($"TimeMod not supported in version {context.Version}");
        }
        
        else
        {
            // Uint <= 48, byte otherwise
            if (context.Version <= 48)
            {
                stream.Write(BitConverter.GetBytes((uint)Value));
            }
            else
            {
                stream.WriteByte((byte)Value);
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        Value = DeserializeStatic(stream, context.Version);
    }

    public static HircType DeserializeStatic(Stream stream, uint version)
    {
        uint initialValue;

        // Uint <= 48, byte otherwise
        if (version <= 48)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            initialValue = BitConverter.ToUInt32(span);
        }
        else
        {
            initialValue = (uint)stream.ReadByte();
        }

        // Handle the two removed type values on higher versions
        if (version > 126 && initialValue > 0x0f)
        {
            initialValue += 2;
        }

        return (HircType)initialValue;
    }
}

public enum HircType : uint
{
    None,
    State,
    Sound,
    Action,
    Event,
    RandomSequenceContainer,
    SwitchContainer,
    ActorMixer,
    Bus,
    LayerContainer,
    MusicSegment,
    MusicTrack,
    MusicSwitch,
    MusicRandomSequence,
    Attenuation,
    DialogueEvent,
    FeedbackBus,
    FeedbackNode,
    FxShareSet,
    FxCustom,
    AuxiliaryBus,
    LFO,
    Envelope,
    AudioDevice,
    TimeMod,
}