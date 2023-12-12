namespace ME3Tweaks.Wwiser.Tests;

public class WwiseBankParserTests
{
    [TestCase("LE3_v134_1.bnk", 134)]
    [TestCase("ME3_v56_1.bnk", 56)]
    public void OnInstantiation_WithFullBank_ParsesVersionCorrectly(string bankFileName, int correctVersion)
    {
        var parser = new WwiseBankParser(TestData.GetTestDataFilePath("WholeBanks", bankFileName));
        Assert.That(parser.Version, Is.EqualTo(correctVersion));
    }

    [TestCase("ME3_v56_1.bnk")]
    [TestCase("ME3_v56_2.bnk")]
    [TestCase("ME3_v56_3.bnk")]
    public async Task FullBank_V56_Reserializes(string filename)
    {
        var parser = new WwiseBankParser(TestData.GetTestDataFilePath("WholeBanks", filename));
        await parser.Deserialize();

        var stream = new MemoryStream();
        await parser.Serialize(stream);
        //TestHelpers.WriteStreamToFile(stream, TestData.GetTestDataFilePath("WholeBanks", "Out1.bnk"));
        var data = TestData.GetTestDataBytes("WholeBanks", filename);
        Assert.That(stream.ToArray(), Is.EquivalentTo(data));
    }
    
    [TestCase("LE3_v134_1.bnk")] // TODO: What is at the end of this file?
    public async Task FullBank_V134_Reserializes(string filename)
    {
        var parser = new WwiseBankParser(TestData.GetTestDataFilePath("WholeBanks", filename));
        await parser.Deserialize();

        var stream = new MemoryStream();
        await parser.Serialize(stream);
        //TestHelpers.WriteStreamToFile(stream, TestData.GetTestDataFilePath("WholeBanks", "Out1.bnk"));
        var data = TestData.GetTestDataBytes("WholeBanks", filename);
        var outData = stream.ToArray();
        Assert.That(outData, Is.EquivalentTo(data.Take(outData.Length)));
    }
}