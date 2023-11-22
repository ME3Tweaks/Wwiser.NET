namespace ME3Tweaks.Wwiser.Tests;

public static class TestHelpers
{
    /// <summary>
    /// Helper factory method to deserialize a piece of the bank tree with the given version
    /// </summary>
    /// <param name="data">Data to deserialize</param>
    /// <param name="version">Wwise version to pass into serializer</param>
    /// <typeparam name="T">Type of class to deserialize in to</typeparam>
    /// <returns></returns>
    public static (BinarySerializer, T) Deserialize<T>(byte[] data, uint version)
    {
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<T>(data, new BankSerializationContext(version));
        return (serializer, result);
    }
}