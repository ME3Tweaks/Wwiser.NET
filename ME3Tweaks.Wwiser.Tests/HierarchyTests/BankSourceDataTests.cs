using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class BankSourceDataTests
{
    [TestCase("BSDV134.bin", 134)]
    [TestCase("BSDV56.bin", 56)]
    [TestCase("BSDV56_2.bin", 56)]
    [TestCase("BSDV44.bin", 44)]
    public void BankSourceData_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"BankSourceData", filename);
        var (_, result) = TestHelpers.Deserialize<BankSourceData>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data.Take(reserialized.Length).ToArray()));
    }
}