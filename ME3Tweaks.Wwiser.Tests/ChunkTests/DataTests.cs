namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class DataTests
{
    [Test]
    public void DataChunk_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Data",@"DATAv134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("DATA"));
            Assert.That(result.ChunkSize, Is.EqualTo(0xF919));
            Assert.That(result.Chunk, Is.InstanceOf<DataChunk>());
        });
        
        var chunk = result.Chunk as DataChunk;
        Assert.That(chunk.Data.Length, Is.EqualTo(0xF919));
    }
    
    [Test]
    public void DataChunk_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"Data",@"DATAv134.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        var outputStream = new MemoryStream();
        serializer.Serialize(outputStream, result, new BankSerializationContext(134));
        outputStream.Position = 0;
        
        Assert.That(outputStream.ToArray(), Is.EqualTo(data));
    }
}