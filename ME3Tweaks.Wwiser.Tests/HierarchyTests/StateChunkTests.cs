using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class StateChunkTests
{
    [Test]
    public void V134_EmptyStateChunkAware_Parses()
    {
        byte[] data = { 00, 00 };
        var (_, result) = TestHelpers.Deserialize<StateChunk_Aware>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.StateGroupsCount.Value, Is.EqualTo(0));
            Assert.That(result.StatePropsCount.Value, Is.EqualTo(0));
        });
    }
}