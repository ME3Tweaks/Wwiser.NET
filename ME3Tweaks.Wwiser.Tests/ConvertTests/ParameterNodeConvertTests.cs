using ME3Tweaks.Wwiser.BankConversion;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Tests.ConvertTests;

public class ParameterNodeConvertTests
{
    [Test]
    public void Convert56to134_Works()
    {
        var from = new BankSerializationContext(56);
        var to = new BankSerializationContext(134);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"56.bin");
        var (_, result) = TestHelpers.Deserialize<NodeBaseParameters>(data, 56);

        var c = new InitialParamsConverter(result);
        Assert.That(c.ShouldConvert(from, to), Is.True);
        c.Convert(from, to);

        var newData = TestHelpers.Serialize(result, 134);
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"134.bin")));
    }
    
    [Test]
    public void Convert134to56_Works()
    {
        var from = new BankSerializationContext(134);
        var to = new BankSerializationContext(56);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"134.bin");
        var (_, result) = TestHelpers.Deserialize<NodeBaseParameters>(data, 134);

        var c = new InitialParamsConverter(result);
        Assert.That(c.ShouldConvert(from, to), Is.True);
        c.Convert(from, to);

        var newData = TestHelpers.Serialize(result, 56);
        TestHelpers.WriteStreamToFile(new MemoryStream(newData), TestData.GetTestDataFilePath("Convert", "ParameterNode", "Out56"));
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"56.bin")));
    }
}