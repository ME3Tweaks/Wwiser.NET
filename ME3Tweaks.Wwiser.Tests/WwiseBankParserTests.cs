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

    [Test]
    public async Task FullBank_V56_Reserializes()
    {
        var parser = new WwiseBankParser(TestData.GetTestDataFilePath("WholeBanks", "ME3_v56_2.bnk"));
        await parser.Deserialize();

        var stream = new MemoryStream();
        await parser.Serialize(stream);
        var data = TestData.GetTestDataBytes("WholeBanks", "ME3_v56_2.bnk");
        Assert.That(stream.ToArray(), Is.EquivalentTo(data));
    }
}