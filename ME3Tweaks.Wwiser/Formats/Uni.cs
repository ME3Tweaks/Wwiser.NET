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
    public bool StoredAsFloat { get; set; }

    [Ignore]
    public float Value => StoredAsFloat ? Float : Integer;
    
    public Uni() { }

    public Uni(float f)
    {
        SetValue(BitConverter.GetBytes(f));
    }

    public Uni(uint i)
    {
        SetValue(BitConverter.GetBytes(i));
    }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        stream.Write(StoredAsFloat ? BitConverter.GetBytes(Float) : BitConverter.GetBytes(Integer));
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();
        
        SetValue(span);
    }

    private void SetValue(byte[] data)
    {
        if (data.Length != 4) throw new ArgumentException();
        SetValue(data.AsSpan());
    }
    
    private void SetValue(Span<byte> span)
    {
        StoredAsFloat = StoreAsFloat(span);
        if (StoredAsFloat)
        {
            Float = BitConverter.ToSingle(span);
            Integer = 0;
        }
        else
        {
            Float = 0f;
            Integer = BitConverter.ToUInt32(span);
        }
    }

    private static bool StoreAsFloat(Span<byte> data)
    {
        uint value = BitConverter.ToUInt32(data);
        return value > 0x10000000;
    }
}