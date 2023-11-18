using System.Text;

namespace ME3Tweaks.Wwiser.Tests.FormatTests;

public class BankStringUtf8Tests
{
    [TestCase("")]
    [TestCase("1234")]
    [TestCase("the quick brown fox jumps over the lazy dog")]
    public void BankString_OnVersionsLowerThan136_SerializesLengthFirst(string testString)
    {
        // Arrange
        var serializer = new BinarySerializer();
        var value = new BankStringUtf8(testString);

        // Act
        var stream = new MemoryStream();
        serializer.Serialize(stream, value, new BankSerializationContext(134));
        stream.Position = 0;

        if (stream.Length < sizeof(uint)) return;
        
        // Assert
        var reader = new BinaryReader(stream, Encoding.UTF8);
        var length = reader.ReadUInt32();
        Assert.That(length, Is.EqualTo(testString.Length));
        
        if (stream.Length < sizeof(uint) + sizeof(char)) return;
        
        var firstL = reader.ReadChar();
        Assert.That(firstL, Is.EqualTo(testString[0]));
    }
    
    [TestCase("")]
    [TestCase("1234")]
    [TestCase("the quick brown fox jumps over the lazy dog")]
    public void BankString_OnVersionsHigherThan136_SerializesStringFirst(string testString)
    {
        // Arrange
        var serializer = new BinarySerializer();
        var value = new BankStringUtf8(testString);

        // Act
        var stream = new MemoryStream();
        serializer.Serialize(stream, value, new BankSerializationContext(144));
        stream.Position = 0;

        if (stream.Length < sizeof(uint)) return;
        
        // Assert
        var reader = new BinaryReader(stream, Encoding.UTF8);
        var length = reader.ReadUInt32();
        Assert.That(length, Is.Not.EqualTo(testString.Length));

        reader.BaseStream.Position = 0;
        var firstL = reader.ReadChar();
        Assert.That(firstL, Is.EqualTo(testString[0]));
    }
}