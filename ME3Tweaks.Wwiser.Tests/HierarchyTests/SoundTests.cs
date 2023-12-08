using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class SoundTests
{
    /*[TestCase("Sound_V134.bin", 134, 0x02, (float)0.0)]
    [TestCase("Sound_V56.bin", 56, 0x02,  (float)-69.15897369384766)]
    public void Sound_ParsesCorrectly(string filename, int version, byte firstScaling, float firstTo)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Attenuation", filename);
        var (serializer, result) = TestHelpers.Deserialize<Attenuation>(data, (uint)version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Curves[0].Scaling.Value, Is.EqualTo((CurveScaling.CurveScalingInner)firstScaling));
            Assert.That(result.Curves[0].Graph[0].To, Is.EqualTo(firstTo));
        });
    }*/
    
    [TestCase("Sound_V134.bin", 134)]
    [TestCase("Sound_V44.bin", 44)]
    public void Sound_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Sound", filename);
        var (_, result) = TestHelpers.Deserialize<Sound>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}