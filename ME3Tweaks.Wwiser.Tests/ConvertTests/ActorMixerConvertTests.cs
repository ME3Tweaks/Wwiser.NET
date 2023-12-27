using ME3Tweaks.Wwiser.BankConversion;
using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.ConvertTests;

public class ActorMixerConvertTests
{
    [Test]
    public void Convert56to134_Works()
    {
        var from = new BankSerializationContext(56);
        var to = new BankSerializationContext(134);
        var initialParams = new InitialParamsConverter(from, to);
        var baseParams = new NodeBaseParamsStateConverter(from, to);
        var rtpcConverter = new RtpcConverter(from, to);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"ActorMixer", @"V56.bin");
        var (_, item) = TestHelpers.Deserialize<HircItemContainer>(data, 56);
        if (item.Item is not IHasNode node) throw new ArgumentException();
        initialParams.Convert(node.NodeBaseParameters);
        baseParams.Convert(node.NodeBaseParameters);
        rtpcConverter.Convert(node.NodeBaseParameters.Rtpc);
        
        var newData = TestHelpers.Serialize(item, 134);
        Assert.That(newData.Length, Is.EqualTo(TestData.GetTestDataBytes(@"Convert", @"ActorMixer", @"V134.bin").Length));
    }
}