namespace ME3Tweaks.Wwiser.Tests;

public class WwiseVersionParsingTests
{
    [TestCase("LE3_v134_1.bnk", 134)]
    [TestCase("ME3_v56_1.bnk", 56)]
    public void WwiseBankParser_FullBank_ParsesVersionCorrectly(string bankFileName, int correctVersion)
    {
        var stream = TestData.GetTestDataStream("WholeBanks", bankFileName);
        var parser = new WwiseBankParser();

        var version = parser.GetWwiseVersionNumber(stream);
        Assert.That(version, Is.EqualTo(correctVersion));
    }
}