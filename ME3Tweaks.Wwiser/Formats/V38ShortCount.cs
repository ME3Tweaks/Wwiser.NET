using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

public class V38ShortCount : IBinarySerializable
{
    [Ignore]
    public ushort Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 38)
        {
            stream.Write(BitConverter.GetBytes((uint)Value));
        }
        else stream.Write(BitConverter.GetBytes(Value));
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 38)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (byte)BitConverter.ToUInt32(span);
        }
        else
        {
            Span<byte> span = stackalloc byte[2];
            var read = stream.Read(span);
            if (read != 2) throw new Exception();
            Value = BitConverter.ToUInt16(span);
        }
    }
}