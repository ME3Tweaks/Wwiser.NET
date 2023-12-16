using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class V36ByteCount : IBinarySerializable
{
    [Ignore]
    public byte Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36)
        {
            stream.Write(BitConverter.GetBytes((uint)Value));
        }
        else stream.WriteByte(Value);
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (byte)BitConverter.ToUInt32(span);
        }
        else Value = (byte)stream.ReadByte();
    }
}