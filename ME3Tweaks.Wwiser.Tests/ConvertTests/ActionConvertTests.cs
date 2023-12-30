using Action = ME3Tweaks.Wwiser.Model.Hierarchy.Action;

namespace ME3Tweaks.Wwiser.Tests.ConvertTests;

public class ActionTests
{
    [Test]
    public void ConvertActionItem_56to134_Works()
    {
        var from = new BankSerializationContext(56);
        var to = new BankSerializationContext(134);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"Action", @"SetPitch_V56.bin");
        var (_, result) = TestHelpers.Deserialize<Action>(data, 56);

        var newData = TestHelpers.Serialize(result, 134);
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"Action", @"SetPitch_V134.bin")));
    }
    
    [Test]
    public void ConvertActionItem_134to56_Works()
    {
        var from = new BankSerializationContext(134);
        var to = new BankSerializationContext(56);
        
        var data = TestData.GetTestDataBytes(@"Convert", @"Action", @"SetPitch_V134.bin");
        var (_, result) = TestHelpers.Deserialize<Action>(data, 134);

        var newData = TestHelpers.Serialize(result, 56);
        Assert.That(newData, Is.EquivalentTo(TestData.GetTestDataBytes(@"Convert", @"Action", @"SetPitch_V56.bin")));
    }
}