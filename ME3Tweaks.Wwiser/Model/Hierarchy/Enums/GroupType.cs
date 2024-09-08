using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class GroupType : IBinarySerializable
{
    public GroupTypeInner Value { get; set; }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 89)
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
        if (version <= 89)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (GroupTypeInner)BitConverter.ToUInt32(span);
        }
        else
        {
            Value = (GroupTypeInner)stream.ReadByte();
        }
    }
    
    public enum GroupTypeInner : uint
    {
        Switch,
        State
    }
}