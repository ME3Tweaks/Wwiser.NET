using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class V126ByteCount : IBinarySerializable
{
    [Ignore]
    public byte Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 126)
        {
            stream.WriteByte(Value);
        }
        else stream.Write(BitConverter.GetBytes((ushort)Value));
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 126)
        {
            Value = (byte)stream.ReadByte();
        }
        else
        {
            Span<byte> span = stackalloc byte[2];
            var read = stream.Read(span);
            if (read != 2) throw new Exception();
            Value = (byte)BitConverter.ToUInt16(span);
        }
    }
}