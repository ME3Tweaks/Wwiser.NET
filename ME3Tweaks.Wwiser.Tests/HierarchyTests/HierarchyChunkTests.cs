﻿using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HierarchyChunkTests
{
    [TestCase("HIRCv44.bin", 44)]
    [TestCase("HIRCv56.bin", 56)]
    [TestCase("HIRCv134.bin", 134)]
    public void SimpleHircChunk_Reserializes(string fileName, int version)
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", fileName);
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, version);
        
        var reserialized = TestHelpers.Serialize(result, version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
    
    [Test]
    public void SimpleHircChunk_v44_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv44.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 44);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("HIRC"));
            Assert.That(result.Chunk, Is.InstanceOf<HierarchyChunk>());
        });

        if (result.Chunk is HierarchyChunk hirc)
        {
            Assert.Multiple(() =>
            {
                Assert.That(hirc.ItemCount, Is.EqualTo(1));
                Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainer>());

                var hircItemContainer = hirc.Items[0];
                Assert.That(hircItemContainer.Type.Value, Is.EqualTo(HircType.Event));
                Assert.That(hircItemContainer.Item, Is.InstanceOf<Event>());
            });
        }
    }
    
    [Test]
    public void SimpleHircChunk_v56_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv56.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 56);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("HIRC"));
            Assert.That(result.Chunk, Is.InstanceOf<HierarchyChunk>());
        });

        if (result.Chunk is HierarchyChunk hirc)
        {
            Assert.Multiple(() =>
            {
                Assert.That(hirc.ItemCount, Is.EqualTo(1));
                Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainer>());

                var hircItemContainer = hirc.Items[0];
                Assert.That(hircItemContainer.Type.Value, Is.EqualTo(HircType.Event));
                Assert.That(hircItemContainer.Item, Is.InstanceOf<Event>());
            });
        }
    }
    
    [Test]
    public void SimpleHircChunk_v134_Parses()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"SmallFullChunks", @"HIRCv134.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 134);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Tag, Is.EqualTo("HIRC"));
            Assert.That(result.Chunk, Is.InstanceOf<HierarchyChunk>());
        });

        if (result.Chunk is HierarchyChunk hirc)
        {
            Assert.Multiple(() =>
            {
                Assert.That(hirc.ItemCount, Is.EqualTo(1));
                Assert.That(hirc.Items[0], Is.InstanceOf<HircItemContainer>());

                var hircItemContainer = hirc.Items[0];
                Assert.That(hircItemContainer.Type.Value, Is.EqualTo(HircType.Event));
                Assert.That(hircItemContainer.Item, Is.InstanceOf<Event>());
            });
        }
    }

    [Test]
    public void BigHircChunk_Reserializes()
    {
        var data = TestData.GetTestDataBytes(@"Hierarchy", @"LargeFullChunks", @"HIRC_V56.bin");
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, 56);
        
        var reserialized = TestHelpers.Serialize(result, 56);
        TestHelpers.WriteStreamToFile(new MemoryStream(reserialized), TestData.GetTestDataFilePath(@"Hierarchy", @"LargeFullChunks", @"Out56.bin"));
        Assert.That(reserialized, Is.EqualTo(data));
    }
}