using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HierarchyBankTests
{
    [Test]
    public void v44_LoadsCorrectItemContainer()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"Items", @"Eventv44.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 44);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("HIRC"));
            Assert.That(result.Chunk, Is.InstanceOf<HierarchyChunk>());
        });
        
        var hirc = result.Chunk as HierarchyChunk;
        Assert.Multiple(() =>
        {
            Assert.That(hirc.ItemCount, Is.EqualTo(1));
            Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainer>());
            
            var hircItemContainer = hirc.Items[0] as HircItemContainer;
            Assert.That(hircItemContainer.Type, Is.EqualTo(HircType.Event));
            Assert.That(hircItemContainer.Item, Is.InstanceOf<HircEventItem>());
        });
    }
}