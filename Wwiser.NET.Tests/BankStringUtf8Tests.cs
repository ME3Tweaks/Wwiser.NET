using System.Runtime.Serialization;

namespace ME3Tweaks.Wwiser.Tests;

public class BankStringUtf8Tests
{
    [Test]
    [TestCase("")]
    [TestCase("ABCD")]
    [TestCase("the quick brown fox jumps over the lazy dog")]
    public void BankString_SerializesLength(string testString)
    {
        var serializer = new BinarySerializer();
        var value = new BankStringUtf8(testString);

        var stream = new MemoryStream();
        serializer.Serialize(stream, value, new BankSerializationContext(134));
        stream.Position = 0;

        uint[] length = new uint[1];
        byte[] buffer = stream.ToArray();
        Buffer.BlockCopy(buffer, 0, length, 0, 1);
        Assert.AreEqual(testString.Length, length[0]);
        
    }
}