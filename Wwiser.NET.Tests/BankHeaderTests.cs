namespace ME3Tweaks.Wwiser.Tests;

public class BankHeaderTests
{
    [Test]
    public void V134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"BankHeader\v134.bin");
        var serializer = new BinarySerializer();
        var result = serializer.Deserialize<ChunkContainer>(data, new BankSerializationContext(134));
        
        Assert.AreEqual("BKHD", result.Tag);
        Assert.IsInstanceOf<BankHeader>(result.Chunk);
        
        var chunk = result.Chunk as BankHeader;
        Assert.AreEqual(134, chunk.BankGeneratorVersion);
        Assert.AreEqual(7572, chunk.ProjectId);
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
        
        Assert.AreEqual(data, outputStream.ToArray());
    }
}