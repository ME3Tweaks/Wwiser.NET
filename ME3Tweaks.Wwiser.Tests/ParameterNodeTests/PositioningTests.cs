﻿using ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class PositioningTests
{
    [Test]
    public void Positioning_V134_Parses()
    {
        var byteData = new byte[] { 0x03, 0x0A };
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(byteData, 134);
        Assert.Multiple(() =>
        {
            Assert.That(result.HasPositioning, Is.True);
            Assert.That(result.Has3DPositioning, Is.True);
            
            Assert.That(result.Flags, Is.EqualTo(PositioningChunk.PositioningFlags.PositioningInfoOverrideParent | PositioningChunk.PositioningFlags.HasListenerRelativeRouting));
            Assert.That(result.PanningType, Is.EqualTo(PositioningChunk.SpeakerPanningType.DirectSpeakerAssignment));
            Assert.That(result.PositionType, Is.EqualTo(PositioningChunk.PositionType3D.Emitter));
        });
        
    }
    
    [TestCase(new byte[] {0x03, 0x0A}, 134)]
    [TestCase(new byte[] {0x00}, 44)]
    public void Positioning_V134_Reserializes(byte[] data, int version)
    {
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
        
    }
    
    [Test]
    public void Positioning_V56_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"ParameterNode", "Positioning_V56.bin");
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(data, 56);

        var reserialized = TestHelpers.Serialize(result, 56);
        Assert.That(reserialized, Is.EquivalentTo(data));
        
    }
}