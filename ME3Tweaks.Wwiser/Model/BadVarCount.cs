using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

/// <summary>
/// Shitty hack JUST FOR NOW. This is going to blow up in my face.
/// </summary>
/// <remarks>
/// See wmodel.py line 428 for what this needs to be.
/// I think it's a space-saving uint - only uses relevant bytes
/// </remarks>
public class BadVarCount : IBinarySerializable
{
    [Ignore]
    public uint Value { get; set; }

    [Ignore] private bool _isOneBit;

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        if (_isOneBit)
        {
            stream.WriteByte((byte)Value);
        }
        else
        {
            stream.Write(BitConverter.GetBytes(Value));
        }
    }
    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (span[1] == 0x00 && span[2] == 0x00 && span[3] == 0x00)
        {
            Value = BitConverter.ToUInt32(span);
        }
        else
        {
            Value = span[0];
            stream.Position -= 3;
            _isOneBit = true;
        }
    }
}