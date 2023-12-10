using ME3Tweaks.Wwiser.Model.Action;
using ME3Tweaks.Wwiser.Model.RTPC;
using Action = ME3Tweaks.Wwiser.Model.Hierarchy.Action;

namespace ME3Tweaks.Wwiser.Tests.ActionTests;

public class PlayTests
{
    [Test]
    public void Play_V134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Action",@"Play_V134.bin");
        var (_, result) = TestHelpers.Deserialize<Action>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Type.Value, Is.EqualTo(ActionTypeValue.Play));
            Assert.That(result.IsBus, Is.EqualTo(false));
            Assert.That(result.ActionParams, Is.InstanceOf<Play>());

            var play = result.ActionParams as Play;
            Assert.That(play!.Params.CurveInterpolation, Is.EqualTo(CurveInterpolation.Linear));
        });
    }
    
    [TestCase("Play_V134.bin", 134)]
    [TestCase("Play_V56.bin", 56)]
    public void Play_Reserializes(string file, int version)
    {
        var data = TestData.GetTestDataBytes(@"Action",file);
        var (_, result) = TestHelpers.Deserialize<Action>(data, version);
        
        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EqualTo(data));
    }
}