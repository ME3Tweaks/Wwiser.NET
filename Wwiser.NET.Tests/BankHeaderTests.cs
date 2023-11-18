namespace ME3Tweaks.Wwiser.Tests;

public class BankHeaderTests
{
    [Test]
    public void V134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"BankHeader\v134.bin");
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(134));
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("BKHD"));
            Assert.That(result.Chunk, Is.InstanceOf<BankHeader>());
        });
        
        var chunk = result.Chunk as BankHeader;
        Assert.Multiple(() =>
        {
            Assert.That(chunk.BankGeneratorVersion, Is.EqualTo(134));
            Assert.That(chunk.ProjectId, Is.EqualTo(7572));
        });
    }

    [Test]
    public void V56_Parses()
    {
        var data = TestData.GetTestDataBytes(@"BankHeader\v56.bin");
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(56));
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("BKHD"));
            Assert.That(result.ChunkSize, Is.EqualTo(0x18));
            Assert.That(result.Chunk, Is.InstanceOf<BankHeader>());
        });
        
        var chunk = result.Chunk as BankHeader;
        Assert.Multiple(() =>
        {
            Assert.That(chunk.BankGeneratorVersion, Is.EqualTo(56));
            Assert.That(chunk.SoundBankId, Is.EqualTo(1564500913));
        });
    }

    [Test]
    public void V134_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"BankHeader\v134.bin");
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(134));
        
        var outputStream = new MemoryStream();
        serializer.Serialize(outputStream, result);
        outputStream.Position = 0;
        
        Assert.That(outputStream.ToArray(), Is.EqualTo(data));
    }
}