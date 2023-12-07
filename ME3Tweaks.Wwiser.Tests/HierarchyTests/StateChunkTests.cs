using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.State;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class StateChunkTests
{
    [TestCase(new byte[] {00, 00}, 134)]
    [TestCase(new byte[] {00, 00, 00, 00}, 120)]
    public void Empty_MultipleVersions_Parses(byte[] data, int version)
    {
        var (_, result) = TestHelpers.Deserialize<StateChunk>(data, version);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.StateGroupsCount.Value, Is.EqualTo(0));
            Assert.That(result.StatePropsCount.Value, Is.EqualTo(0));
        });
    }
}