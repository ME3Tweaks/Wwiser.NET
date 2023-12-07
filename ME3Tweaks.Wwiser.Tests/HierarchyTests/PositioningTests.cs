using ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class PositioningTests
{
    [TestCase]
    public void Positioning_V134_Parses()
    {
        var byteData = new byte[] { 0x03, 0x0A };
        var version = 134;
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(byteData, version);
        Assert.Multiple(() =>
        {
            Assert.That(result.HasPositioning, Is.True);
            Assert.That(result.Has3DPositioning, Is.True);
            
            Assert.That(result.Flags, Is.EqualTo(PositioningChunk.PositioningFlags.PositioningInfoOverrideParent | PositioningChunk.PositioningFlags.HasListenerRelativeRouting));
            Assert.That(result.PanningType, Is.EqualTo(PositioningChunk.SpeakerPanningType.DirectSpeakerAssignment));
            Assert.That(result.PositionType, Is.EqualTo(PositioningChunk.PositionType3D.Emitter));
        });
        
    }
    
    [TestCase]
    public void Positioning_V134_Reserializes()
    {
        var byteData = new byte[] { 0x03, 0x0A };
        var version = 134;
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(byteData, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(byteData));
        
    }
}