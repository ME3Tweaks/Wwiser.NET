using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class StreamType : IBinarySerializable
{
    [Ignore]
    public StreamTypeInner Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var valueToWrite = Value;
        if (version >= 112)
        {
            if (valueToWrite is StreamTypeInner.Streaming)
            {
                valueToWrite = StreamTypeInner.PrefetchStreaming;
            }
            else if (valueToWrite is StreamTypeInner.PrefetchStreaming)
            {
                valueToWrite = StreamTypeInner.Streaming;
            }
        }

        if (version <= 89)
        {
            stream.Write(BitConverter.GetBytes((uint)valueToWrite));
        }
        else
        {
            stream.WriteByte((byte)valueToWrite);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;

        Value = DeserializeStatic(stream, version);
    }

    public static StreamTypeInner DeserializeStatic(Stream stream, uint version)
    {
        byte value;
        if (version <= 89)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            value = (byte)BitConverter.ToUInt32(span);
        }
        else
        {
            value = (byte)stream.ReadByte();
        }

        if (version >= 112)
        {
            switch (value)
            {
                case 1:
                    value++;
                    break;
                case 2:
                    value--;
                    break;
            }
        }

        return (StreamTypeInner)value;
    }

    public enum StreamTypeInner : byte
    {
        DataBnk,
        Streaming, // PrefetchStreaming >= 112 - AK WHY
        PrefetchStreaming, // Streaming >= 112 - AK WHY
    }
}