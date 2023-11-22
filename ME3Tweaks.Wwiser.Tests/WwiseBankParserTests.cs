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
}