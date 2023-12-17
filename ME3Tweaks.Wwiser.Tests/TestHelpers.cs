namespace ME3Tweaks.Wwiser.Tests;

public static class TestHelpers
{
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, BankSerializationContext bsc)
    {
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<T>(data, bsc);
        return (serializer, result);
    }
    
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, uint version,  bool useModulator = false)
    {
        return Deserialize<T>(data, new BankSerializationContext(version, useModulator));
    }
    
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, int version)
    {
        return Deserialize<T>(data, (uint)version);
    }
    
    public static (BinarySerializer, T) Deserialize<T>(byte data, int version, bool useModulator = false)
    {
        var dataArr = new [] { data };
        return Deserialize<T>(dataArr, (uint)version, useModulator);
    }

    public static byte[] Serialize(object data, BankSerializationContext bsc)
    {
        var serializer = new BinarySerializer();
        var stream = new MemoryStream();
        serializer.Serialize(stream, data, bsc);
        stream.Position = 0;

        return stream.ToArray();
    }

    public static byte[] Serialize(object data, uint version,  bool useModulator = false)
    {
        return Serialize(data, new BankSerializationContext(version, useModulator));
    }
    
    public static byte[] Serialize(object data, int version,  bool useModulator = false)
    {
        return Serialize(data, (uint)version, useModulator);
    }

    public static void WriteStreamToFile(Stream stream, string filepath)
    {
        stream.Seek(0, SeekOrigin.Begin);

        using var fs = new FileStream(filepath, FileMode.OpenOrCreate);
        stream.CopyTo(fs);
    }
}