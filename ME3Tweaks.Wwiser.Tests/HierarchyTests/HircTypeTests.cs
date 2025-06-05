using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HircTypeTests
{
    [TestCase(HircType.State, 0x01)]
    [TestCase(HircType.DialogueEvent, 0x0f)]
    [TestCase(HircType.FxShareSet, 0x10)]
    [TestCase(HircType.AudioDevice, 0x15)]
    [TestCase(HircType.TimeMod, 0x16)]
    public void HircType_V136_ReserializesCorrectly(HircType type, int expected)
    {
        var data = new HircSmartType { Value = type };
        var output = TestHelpers.Serialize(data, 134);

        var parsed = (uint)output[0];

        var (_, result) = TestHelpers.Deserialize<HircSmartType>(output, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.EqualTo((uint)expected));
            Assert.That(result.Value, Is.EqualTo(type));
        });
    }
    
    [TestCase(HircType.State, 0x01)]
    [TestCase(HircType.DialogueEvent, 0x0f)]
    [TestCase(HircType.FeedbackBus, 0x10)]
    [TestCase(HircType.FeedbackNode, 0x11)]
    [TestCase(HircType.FxShareSet, 0x12)]
    [TestCase(HircType.AudioDevice, 0x17)]
    public void HircType_V48_ReserializesCorrectly(HircType type, int expected)
    {
        var data = new HircSmartType { Value = type };
        var output = TestHelpers.Serialize(data, 48);

        var parsed = BitConverter.ToUInt32(output);

        var (_, result) = TestHelpers.Deserialize<HircSmartType>(output, 48);
        
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.EqualTo((uint)expected));
            Assert.That(result.Value, Is.EqualTo(type));
        });
    }
    
    [TestCase(HircType.FeedbackNode, 134)]
    [TestCase(HircType.FeedbackBus, 134)]
    [TestCase(HircType.TimeMod, 56)]
    public void SerializingIncompatibleTypesForVersion_ThrowsError(HircType type, int version)
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TestHelpers.Serialize(new HircSmartType { Value = type }, version);
        });
    }

    [Test]
    public void LowerVersions_SerializesUint()
    {
        var result = TestHelpers.Serialize(
            new HircSmartType { Value = HircType.AudioDevice }, 48);
        
        Assert.That(result.Length, Is.EqualTo(4));

        var converted = BitConverter.ToUInt32(result);
        Assert.That(converted, Is.EqualTo((uint)HircType.AudioDevice));
    }
    
    [Test]
    public void HigherVersions_SerializesByte()
    {
        var result = TestHelpers.Serialize(
            new HircSmartType { Value = HircType.AudioDevice }, 49);
        
        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo((byte)HircType.AudioDevice));
    }
}