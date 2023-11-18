namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class MediaIndexTests
{
    [Test]
    public void V134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"MediaIndex",@"DIDXv134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("DIDX"));
            Assert.That(result.ChunkSize, Is.EqualTo(0x1A4));
            Assert.That(result.Chunk, Is.InstanceOf<MediaIndexChunk>());
        });
        
        var chunk = result.Chunk as MediaIndexChunk;
        var firstElement = new MediaHeader() { Id = 6098048, Offset = 0x00, Size = 0x12BD };
        Assert.Multiple(() =>
        {
            Assert.That(chunk.LoadedMedia.Count, Is.EqualTo(35));
            Assert.That(chunk.LoadedMedia[0].Id, Is.EqualTo(firstElement.Id));
            Assert.That(chunk.LoadedMedia[0].Offset, Is.EqualTo(firstElement.Offset));
            Assert.That(chunk.LoadedMedia[0].Size, Is.EqualTo(firstElement.Size));
        });
    }
    
    [Test]
    public void V134_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"MediaIndex",@"DIDXv134.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        var outputStream = new MemoryStream();
        serializer.Serialize(outputStream, result);
        outputStream.Position = 0;
        
        Assert.That(outputStream.ToArray(), Is.EqualTo(data));
    }
}