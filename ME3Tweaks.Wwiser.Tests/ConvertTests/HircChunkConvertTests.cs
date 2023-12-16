using ME3Tweaks.Wwiser.BankConversion;
using Ignore = NUnit.Framework.IgnoreAttribute;

namespace ME3Tweaks.Wwiser.Tests.ConvertTests;

[Ignore("Can't properly convert whole HIRC chunk yet")]
public class HircChunkConvertTests
{
    [Test]
    public void Convert56to134_Works()
    {
        var from = new BankSerializationContext(56);
        var to = new BankSerializationContext(134);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"HIRC", @"56.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 56);

        var c = new HircConverter(result.Chunk as HierarchyChunk);
        c.Convert(from, to);

        var newData = TestHelpers.Serialize(result, 134);
        TestHelpers.WriteStreamToFile(new MemoryStream(newData), TestData.GetTestDataFilePath("Convert", "HIRC", "Out134"));
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"HIRC", @"134.bin")));
    }
}