using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class MusicTrackType : IBinarySerializable
{
    public MusicTrackTypeInner Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        
        if(Value is MusicTrackTypeInner.Switch && context.Version < 89)
        {
            throw new NotSupportedException($"MusicTrackType.Switch is not supported in version {context.Version}");
        }

        if (context.Version <= 89)
        {
            stream.Write(BitConverter.GetBytes((uint)Value));
        }
        else
        {
            stream.WriteByte((byte)Value);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        Value = DeserializeStatic(stream, context.Version);
    }

    public static MusicTrackTypeInner DeserializeStatic(Stream stream, uint version)
    {
        if (version > 89) return (MusicTrackTypeInner)stream.ReadByte();
        
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();
        return (MusicTrackTypeInner)BitConverter.ToUInt32(span);
    }
    
    public enum MusicTrackTypeInner : byte
    {
        Normal,
        Random,
        Sequence,
        Switch
    }
}

