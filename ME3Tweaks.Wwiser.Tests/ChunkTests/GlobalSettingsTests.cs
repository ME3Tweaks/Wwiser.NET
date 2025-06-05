using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Tests.ChunkTests;

public class GlobalSettingsTests
{
    [TestCase("STMG_v134.bin", 134)]
    [TestCase("STMG_v56.bin", 56)]
    public void STMG_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"GlobalSettings", filename);
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }

    [Test]
    public void STMG_v134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"GlobalSettings", @"STMG_v134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);

        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("STMG"));
            Assert.That(result.Chunk, Is.InstanceOf<GlobalSettingsChunk>());
        });

        if (result.Chunk is GlobalSettingsChunk stmg)
        {
            Assert.Multiple(() =>
            {
                Assert.That(stmg.StateGroupCount, Is.EqualTo(12));
                Assert.That(stmg.ParamCount, Is.EqualTo(84));

                var switchGroup = stmg.SwitchGroups[0];
                Assert.That(switchGroup.RtpcType, Is.Not.Null);
                Assert.That(switchGroup.RtpcType!.Value, Is.EqualTo(RtpcType.RtpcTypeInner.GameParameter));
                Assert.That(switchGroup.Graph[0].Interp, Is.EqualTo(CurveInterpolation.Constant));

                var param = stmg.Params[2];
                Assert.That(param.Value, Is.EqualTo(-96.0));
            });
        }
    }

    [Test]
    public void STMG_v56_Parses()
    {
        var data = TestData.GetTestDataBytes(@"GlobalSettings", @"STMG_v56.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 56);

        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("STMG"));
            Assert.That(result.Chunk, Is.InstanceOf<GlobalSettingsChunk>());
        });

        if (result.Chunk is GlobalSettingsChunk stmg)
        {
            Assert.Multiple(() =>
            {
                Assert.That(stmg.StateGroupCount, Is.EqualTo(9));
                Assert.That(stmg.ParamCount, Is.EqualTo(84));

                var switchGroup = stmg.SwitchGroups[0];
                Assert.That(switchGroup.RtpcID, Is.EqualTo(3196713380u));
                Assert.That(switchGroup.RtpcType, Is.Null);
                Assert.That(switchGroup.Graph[0].Interp, Is.EqualTo(CurveInterpolation.Constant));
                Assert.That(switchGroup.Graph[1].From, Is.EqualTo(1.0));

                var param = stmg.Params[2];
                Assert.That(param.RtpcId, Is.EqualTo(121857564u));
                Assert.That(param.Value, Is.EqualTo(-96.0));
            });
        }
    }
}