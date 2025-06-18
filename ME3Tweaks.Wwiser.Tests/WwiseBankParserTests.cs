namespace ME3Tweaks.Wwiser.Tests;

public class WwiseBankParserTests
{
    [TestCase("LE3_v134_1.bnk", 134)]
    [TestCase("ME3_v56_1.bnk", 56)]
    public void ReadHeaderInfo_OnFullBank_ParsesVersionCorrectly(string bankFileName, int correctVersion)
    {
        var stream = TestData.GetTestDataStream("WholeBanks", bankFileName);
        var (version, _) = WwiseBankParser.ReadWwiseHeaderInfo(stream);
        Assert.That(version, Is.EqualTo(correctVersion));
        Assert.That(stream.Position, Is.EqualTo(0));
    }

    [Test]
    public void FullBank_V56_Reserializes_Synchronous()
    {
        var data = TestData.GetTestDataBytes("WholeBanks", "ME3_v56_1.bnk");
        var inputStream = new MemoryStream(data);
        var outputStream = new MemoryStream();
        
        var bank = WwiseBankParser.Deserialize(inputStream);
        WwiseBankParser.Serialize(bank, outputStream);
        
        Assert.That(outputStream.ToArray(), Is.EquivalentTo(data));
    }

    [TestCase("ME3_v56_1.bnk")]
    [TestCase("ME3_v56_2.bnk")]
    [TestCase("ME3_v56_3.bnk")]
    [TestCase("LE3_v134_1.bnk")] // TODO: What is at the end of this file?
    public async Task FullBank_Reserializes_Async(string filename)
    {
        var data = TestData.GetTestDataBytes("WholeBanks", filename);
        var inputStream = new MemoryStream(data);
        var outputStream = new MemoryStream();
        
        var bank = await WwiseBankParser.DeserializeAsync(inputStream);
        await WwiseBankParser.SerializeAsync(bank, outputStream);
        
        TestHelpers.WriteStreamToFile(outputStream, TestData.GetTestDataFilePath("WholeBanks", "Out1.bnk"));
        Assert.That(outputStream.ToArray(), Is.EquivalentTo(data));
    }
}