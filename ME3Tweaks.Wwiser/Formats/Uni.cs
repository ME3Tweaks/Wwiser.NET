using BinarySerialization;

namespace ME3Tweaks.Wwiser.Formats;

/// <summary>
/// Weird union between f32 and i32. I hate this so much.
/// </summary>
public class Uni : IBinarySerializable
{
    [Ignore]
    public float Float { get; set; }
    
    [Ignore]
    public uint Integer { get; set; }

    [Ignore]
    public float Value => Float == 0.0f ? Integer : Float;
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        if (Float == 0.0f)
        {
            stream.Write(BitConverter.GetBytes(Integer));
        }
        else
        {
            stream.Write(BitConverter.GetBytes(Float));
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();

        uint value = BitConverter.ToUInt32(span);
        if (value > 0x10000000)
        {
            Float = BitConverter.ToSingle(span);
        }
        else Integer = value;
    }
    
    public Uni() { }

    public Uni(float f)
    {
        Float = f;
    }

    public Uni(uint i)
    {
        Integer = i;
    }
}