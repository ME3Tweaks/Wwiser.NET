using System;
using System.IO;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

/// <summary>
/// Weirdly serialized count in different versions.
/// &lt;=122 it's a normal uint, otherwise some sort of space-saving uint
/// </summary>
public class VarCount : IBinarySerializable
{
    [Ignore]
    public uint Value { get; set; }

    //TODO: THIS!
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        
    }
    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {

        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (context.Version <= 122)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
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
        stream.Read(cur);
        value = (uint)(cur[0] & 0x7F);
        var max = 0;
        while ((cur[0] & 0x80) != 0 && max < 10)
        {
            stream.Read(cur);
            value = (value << 7) | (uint)(cur[0] & 0x7F);
            max++;
        }

        return value;
    }
}