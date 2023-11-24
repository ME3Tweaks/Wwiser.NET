using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HierarchyBankTests
{
    [Test]
    public void v44_LoadsCorrectItemContainer()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv44.bin");
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
    
    [Test]
    public void v134_LoadsCorrectItemContainer()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv134.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("HIRC"));
            Assert.That(result.Chunk, Is.InstanceOf<HierarchyChunk>());
        });
        
        var hirc = result.Chunk as HierarchyChunk;
        Assert.Multiple(() =>
        {
            Assert.That(hirc.ItemCount, Is.EqualTo(1));
            Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainerV128>());
            
            var hircItemContainer = hirc.Items[0] as HircItemContainerV128;
            Assert.That(hircItemContainer.Type, Is.EqualTo(HircType128.Event));
            Assert.That(hircItemContainer.Item, Is.InstanceOf<HircEventItem122>());
        });
    }
}