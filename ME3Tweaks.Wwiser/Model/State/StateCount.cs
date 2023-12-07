using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

/// <summary>
/// Weirdly serialized count in different versions.
/// &lt;=122 it's a normal uint, otherwise some sort of space-saving uint
/// </summary>
public class StateCount : IBinarySerializable
{
    [Ignore]
    public uint Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (context.Version > 122)
        {
            VarCount.WriteResizingUint(stream, Value);
        }
        else if (context.Version is > 36 and <= 52)
        {
            stream.Write(BitConverter.GetBytes((ushort)Value));
        }
        else
        {
            stream.Write(BitConverter.GetBytes(Value));
        }
    }
    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {

        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (context.Version > 122)
        {
            Value = VarCount.ReadResizingUint(stream);
        }
        else if (context.Version is > 36 and <= 52)
        {
            Span<byte> span = stackalloc byte[2];
            var read = stream.Read(span);
            if (read != 2) throw new Exception();
            Value = BitConverter.ToUInt16(span);
        }
        else
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = BitConverter.ToUInt32(span);
        }
    }
}