﻿namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class MediaIndexTests
{
    [Test]
    public void DIDX_V134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"MediaIndex",@"DIDXv134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("DIDX"));
            Assert.That(result.ChunkSize, Is.EqualTo(0x1A4));
            Assert.That(result.Chunk, Is.InstanceOf<MediaIndexChunk>());
        });
        
        var firstElement = new MediaHeader() { Id = 6098048, Offset = 0x00, Size = 0x12BD };
        if (result.Chunk is MediaIndexChunk chunk)
        {
            Assert.Multiple(() =>
            {
                Assert.That(chunk.LoadedMedia.Count, Is.EqualTo(35));
                Assert.That(chunk.LoadedMedia[0].Id, Is.EqualTo(firstElement.Id));
                Assert.That(chunk.LoadedMedia[0].Offset, Is.EqualTo(firstElement.Offset));
                Assert.That(chunk.LoadedMedia[0].Size, Is.EqualTo(firstElement.Size));
            });
        }
    }
    
    [Test]
    public void DIDX_V134_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"MediaIndex",@"DIDXv134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        var reserialized = TestHelpers.Serialize(result, 134);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
}