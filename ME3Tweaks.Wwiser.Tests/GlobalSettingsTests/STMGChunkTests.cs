using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Tests.GlobalSettingsTests;

public class STMGChunkTests
{
    [TestCase("STMG_v134.bin", 134u)]
    public void STMGChunk_Reserializes(string filename, uint version)
    {
        var data = TestData.GetTestDataBytes(@"GlobalSettings", filename);
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, version);

        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
    
    [Test]
    public void STMGChunk_v134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"GlobalSettings", @"STMG_v134.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
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
                Assert.That(switchGroup.RtpcType.Value, Is.EqualTo(RtpcType.RtpcTypeInner.GameParameter));
                Assert.That(switchGroup.Graph[0].Interp, Is.EqualTo(CurveInterpolation.Constant));

                var param = stmg.Params[2];
                Assert.That(param.Value, Is.EqualTo( -96.0));
            });
        }
    }
    
}