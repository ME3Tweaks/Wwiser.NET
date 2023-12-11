using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class SmartValueMeaning : IBinarySerializable
{
    [Ignore]
    public ValueMeaning Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 56)
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
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 56)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (ValueMeaning)BitConverter.ToUInt32(span);
        }
        else
        {
            Value = (ValueMeaning)stream.ReadByte();
        }
    }
        
    public enum ValueMeaning : byte
    {
        Default,
        Independent,
        Offset
    }
}