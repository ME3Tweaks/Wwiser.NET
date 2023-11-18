namespace ME3Tweaks.Wwiser.Tests;

public class StringMappingTests
{
    [Test]
    public void V56_Parses()
    {
        var data = TestData.GetTestDataBytes(Path.Combine(@"StringMapping",@"STIDv56.bin"));
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(56));
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("STID"));
            Assert.That(result.ChunkSize, Is.EqualTo(0x23));
            Assert.That(result.Chunk, Is.InstanceOf<StringMappingChunk>());
        });
        
        var chunk = result.Chunk as StringMappingChunk;
        var firstElement = new BankHashHeader(1564500913, "Wwise_Generic_Gameplay");
        Assert.Multiple(() =>
        {
            Assert.That(chunk.BankIdToFilename.Count, Is.EqualTo(1));
            Assert.That(chunk.BankIdToFilename[0].BankId, Is.EqualTo(firstElement.BankId));
            Assert.That(chunk.BankIdToFilename[0].StringLength, Is.EqualTo(firstElement.FileName.Length));
            Assert.That(chunk.BankIdToFilename[0].FileName, Is.EqualTo(firstElement.FileName));
        });
    }
    
    [Test]
    public void V56_Reserializes()
    {
        var data = TestData.GetTestDataBytes(Path.Combine(@"StringMapping",@"STIDv56.bin"));
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(56));
        
        var outputStream = new MemoryStream();
        serializer.Serialize(outputStream, result);
        outputStream.Position = 0;
        
        Assert.That(outputStream.ToArray(), Is.EqualTo(data));
    }
}