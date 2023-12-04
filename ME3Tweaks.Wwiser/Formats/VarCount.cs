using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

/// <summary>
/// Weirdly serialized count in different versions.
/// &lt;=122 it's a normal uint, otherwise some sort of space-saving uint
/// </summary>
public class VarCount : IBinarySerializable
{
    [Ignore]
    public uint Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (context.Version <= 122)
        {
            stream.Write(BitConverter.GetBytes(Value));
        }
        else
        {
            WriteResizingUint(stream, Value);
        }
    }
    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {

        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (context.Version <= 122)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = BitConverter.ToUInt32(span);
        }
        else
        {
            Value = ReadResizingUint(stream);
            
        }
    }

    // I don't know how this works
    public static uint ReadResizingUint(Stream stream)
    {
        uint value;
        Span<byte> cur = stackalloc byte[1];
        var read = stream.Read(cur);
        if (read != 1) throw new Exception();
        value = (uint)(cur[0] & 0x7F);
        var max = 0;
        while ((cur[0] & 0x80) != 0 && max < 10)
        {
            read = stream.Read(cur);
            if (read != 1) throw new Exception();
            value = (value << 7) | (uint)(cur[0] & 0x7F);
            max++;
        }

        return value;
    }

    public static void WriteResizingUint(Stream stream, uint value)
    {
        //TODO: This
    }
}