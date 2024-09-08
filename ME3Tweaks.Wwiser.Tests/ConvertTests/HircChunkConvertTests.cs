using ME3Tweaks.Wwiser.BankConversion;
using Ignore = NUnit.Framework.IgnoreAttribute;

namespace ME3Tweaks.Wwiser.Tests.ConvertTests;

//[Ignore("Can't properly convert whole HIRC chunk yet")]
public class HircChunkConvertTests
{
    [Test]
    public void ConvertHircChunk_56to134_ReturnsCorrectDataLength()
    {
        var from = new BankSerializationContext(56);
        var to = new BankSerializationContext(134);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"HIRC", @"56.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, from);

        if (result.Chunk is not HierarchyChunk hirc) throw new Exception();
        HircConverter.ConvertHircChunk(hirc, from, to);

        var newData = TestHelpers.Serialize(result, to);
        var data134 = TestData.GetTestDataBytes(@"Convert", @"HIRC", @"134.bin");
        Assert.That(newData.Length, Is.EqualTo(data134.Length));
        
        /*var outfile = TestData.GetTestDataFilePath("Convert", "HIRC", "Out134");
        File.WriteAllText(outfile, string.Empty); // Wipe out file
        TestHelpers.WriteStreamToFile(new MemoryStream(newData), outfile);*/
    }
}