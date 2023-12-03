using ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class PositioningTests
{
    [TestCase]
    public void PositioningParams_V134_Parses()
    {
        var byteData = new byte[] { 0x03, 0x0A };
        var version = 134;
        var (_, result) = TestHelpers.Deserialize<PositioningChunk>(byteData, version);
        Assert.Multiple(() =>
        {
            Assert.That(result.HasPositioning, Is.True);
            Assert.That(result.Has3DPositioning, Is.True);
            
            Assert.That(result.PositioningBits.Value, Is.EqualTo(BitsPositioning.BitsPositioningInner.PositioningInfoOverrideParent | BitsPositioning.BitsPositioningInner.HasListenerRelativeRouting));
            Assert.That(result.PositioningBits.PanningType, Is.EqualTo(BitsPositioning.SpeakerPanningType.DirectSpeakerAssignment));
            Assert.That(result.PositioningBits.PositionType, Is.EqualTo(BitsPositioning.PositionType3D.Emitter));
        });
        
    }
}