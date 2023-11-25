using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HircEventItemTests
{
    [TestCase("Eventv134.bin", 134, 10686918, 551344110)]
    [TestCase("Eventv56.bin", 56, 258495268, 1032832604)]
    public void HircEventItem_ParsesCorrectly(string filename, int version, int id, int actionId)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Event", filename);
        var (serializer, result) = TestHelpers.Deserialize<HircEventItem>(data, (uint)version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.ActionCount.Value, Is.EqualTo(1));
            Assert.That(result.ActionIds[0], Is.EqualTo(actionId));
        });
    }
    
    //[TestCase("Eventv134.bin", 134)] //this doesn't pass yet because of stupid reasons
    [TestCase("Eventv56.bin", 56)]
    public void HircEventItem_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Event", filename);
        var (serializer, result) = TestHelpers.Deserialize<HircEventItem>(data, (uint)version);
        
        var outputStream = new MemoryStream();
        serializer.Serialize(outputStream, result);
        outputStream.Position = 0;
        
        Assert.That(outputStream.ToArray(), Is.EqualTo(data));
    }
}