﻿using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HierarchyChunkTests
{
    [Test]
    public void SimpleChunk_v44_Parses()
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
            Assert.That(hircItemContainer.Type.Value, Is.EqualTo(HircType.Event));
            Assert.That(hircItemContainer.Item, Is.InstanceOf<Event>());
        });
    }
    
    [Test]
    public void SimpleChunk_v56_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv56.bin");
        var (serializer, result) = TestHelpers.Deserialize<ChunkContainer>(data, 56);
        
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
            //Assert.That(hircItemContainer.Type, Is.EqualTo(HircType.Event as byte));
            //Assert.That(hircItemContainer.Item, Is.InstanceOf<HircEventItem>());
        });
    }
    
    [Test]
    public void SimpleChunk_v134_Parses()
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
            Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainer>());
            
            var hircItemContainer = hirc.Items[0] as HircItemContainer;
            Assert.That(hircItemContainer.Type.Value, Is.EqualTo(HircType.Event));
            Assert.That(hircItemContainer.Item, Is.InstanceOf<Event>());
        });
    }
}