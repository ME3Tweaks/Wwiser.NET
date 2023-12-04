using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class AccumType : IBinarySerializable
{
    public AccumTypeInner Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 125)
        {
            if (Value is > AccumTypeInner.Multiply or < AccumTypeInner.Exclusive) throw new NotSupportedException();
            stream.WriteByte((byte)(Value - 1));
        }
        else
        {
            stream.WriteByte((byte)Value);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var initialValue = stream.ReadByte();
        if (version <= 125)
        {
            initialValue += 1;
        }
        Value = (AccumTypeInner)initialValue;
    }
    
    public enum AccumTypeInner : byte 
    {
        None,
        Exclusive,
        Additive,
        Multiply,
        Boolean,
        Maximum,
        Filter
    }
}