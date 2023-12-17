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
        var (_, result) = TestHelpers.Deserialize<NodeBaseParameters>(data, from);

        var c = new InitialParamsConverter(from, to);
        Assert.That(c.ShouldConvert(), Is.True);
        c.Convert(result);

        var newData = TestHelpers.Serialize(result, to);
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"134.bin")));
    }
    
    [Test]
    public void Convert134to56_Works()
    {
        var from = new BankSerializationContext(134);
        var to = new BankSerializationContext(56);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"134.bin");
        var (_, result) = TestHelpers.Deserialize<NodeBaseParameters>(data, from);

        var c = new InitialParamsConverter(from, to);
        Assert.That(c.ShouldConvert(), Is.True);
        c.Convert(result);

        var newData = TestHelpers.Serialize(result, to);
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"ParameterNode", @"56.bin")));
    }
}