namespace ME3Tweaks.Wwiser.Tests;

public static class TestHelpers
{
    /// <summary>
    /// Helper factory method to deserialize a piece of the bank tree with the given version
    /// </summary>
    /// <param name="data">Data to deserialize</param>
    /// <param name="version">Wwise version to pass into serializer</param>
    /// <param name="useModulator"></param>
    /// <typeparam name="T">Type of class to deserialize in to</typeparam>
    /// <returns></returns>
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, uint version,  bool useModulator = false)
    {
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<T>(data, new BankSerializationContext(version, useModulator));
        return (serializer, result);
    }
    
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, int version,  bool useModulator = false)
    {
        return Deserialize<T>(data, (uint)version);
    }
    
    public static (BinarySerializer, T) Deserialize<T>(byte data, int version, bool useModulator = false)
    {
        var dataArr = new [] { data };
        return Deserialize<T>(dataArr, (uint)version, useModulator);
    }

    public static byte[] Serialize(object data, uint version,  bool useModulator = false)
    {
        var serializer = new BinarySerializer();
        var stream = new MemoryStream();
        serializer.Serialize(stream, data, new BankSerializationContext(version, useModulator));
        stream.Position = 0;

        return stream.ToArray();
    }
    
    public static byte[] Serialize(object data, int version,  bool useModulator = false)
    {
        return Serialize(data, (uint)version, useModulator);
    }
}