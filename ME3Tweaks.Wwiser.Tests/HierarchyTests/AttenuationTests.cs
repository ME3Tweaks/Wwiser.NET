﻿using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class AttenuationTests
{
    [TestCase("AttenuationV134.bin", 134, 0x02, (float)0.0)]
    [TestCase("AttenuationV56.bin", 56, 0x02,  (float)-69.15897369384766)]
    public void Attenuation_ParsesCorrectly(string filename, int version, byte firstScaling, float firstTo)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Attenuation", filename);
        var (_, result) = TestHelpers.Deserialize<Attenuation>(data, version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Curves[0].Scaling.Value, Is.EqualTo((CurveScaling.CurveScalingInner)firstScaling));
            Assert.That(result.Curves[0].Graph[0].To, Is.EqualTo(firstTo));
        });
    }
    
    [TestCase("AttenuationV134.bin", 134)]
    [TestCase("AttenuationV56.bin", 56)]
    public void Attenuation_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy",@"Attenuation", filename);
        var (_, result) = TestHelpers.Deserialize<Attenuation>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}