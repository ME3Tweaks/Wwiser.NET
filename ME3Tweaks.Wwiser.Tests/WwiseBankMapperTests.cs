namespace ME3Tweaks.Wwiser.Tests;

public class WwiseBankMapperTests
{
    [Test]
    public void GetEmbeddedFiles_ParsesDidxAndData()
    {
        var data = new DataChunk()
        {
            Data = TestData.GetTestDataBytes("Data", "DATATester.bin")
        };
        var didx = new MediaIndexChunk()
        {
            LoadedMedia =
            {
                new() { Id = 1, Offset = 0x0, Size = 3 },
                new() { Id = 2, Offset = 0x10, Size = 16 },
                new() { Id = 3, Offset = 0x20, Size = 9 },
                new() { Id = 4, Offset = 0x30, Size = 4 }
            }
        };
        
        var result = new WwiseBankMapper().GetEmbeddedFiles(data, didx);
        
        Assert.That(result, Has.Count.EqualTo(didx.LoadedMedia.Count));
        
        Assert.That(result[0].Id, Is.EqualTo(1));
        var expectedData0 = new byte[] { 0xAA, 0xAA, 0xAA };
        Assert.That(result[0].Data, Is.EquivalentTo(expectedData0));
        
        Assert.That(result[3].Id, Is.EqualTo(4));
        var expectedData3 = new byte[] { 0xDD, 0xDD, 0xDD, 0xDD };
        Assert.That(result[3].Data, Is.EquivalentTo(expectedData3));
    }
    
    [Test]
    public void WriteEmbeddedFiles_CompilesToData()
    {
        var embeddedFiles = new List<EmbeddedFile>
        {
            new() { Id = 1, Data = [0xAA, 0xAA, 0xAA] },
            new() { Id = 2, Data = [0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB] },
            new() { Id = 3, Data = [0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC] },
            new() { Id = 4, Data = [0xDD, 0xDD, 0xDD, 0xDD] }
        };
        
        var result = new WwiseBankMapper().WriteEmbeddedFiles(embeddedFiles);
        
        Assert.That(result.data, Is.Not.Null);

        var expectedCount = embeddedFiles.Take(embeddedFiles.Count - 1).Sum(x => PadTo16(x.Data.Length)) + embeddedFiles.Last().Data.Length;
        Assert.That(result.data.Data.Count, Is.EqualTo(expectedCount));

        var expectedData = TestData.GetTestDataBytes("Data", "DATATester.bin");
        Assert.That(result.data.Data, Is.EquivalentTo(expectedData));
    }
    
    [Test]
    public void WriteEmbeddedFiles_CompilesToDidx()
    {
        var embeddedFiles = new List<EmbeddedFile>
        {
            new() { Id = 1, Data = [0xAA, 0xAA, 0xAA] },
            new() { Id = 2, Data = [0xBB] },
            new() { Id = 3, Data = [0xCC, 0xCC, 0xCC, 0xCC] }
        };
        
        var result = new WwiseBankMapper().WriteEmbeddedFiles(embeddedFiles);
        
        Assert.That(result.didx, Is.Not.Null);
        Assert.That(result.didx.LoadedMedia.Count, Is.EqualTo(embeddedFiles.Count));

        uint offset = 0;
        for (var i = 0; i < embeddedFiles.Count; i++)
        {
            Assert.That(result.didx.LoadedMedia[i].Id, Is.EqualTo(embeddedFiles[i].Id));
            Assert.That(result.didx.LoadedMedia[i].Size, Is.EqualTo((uint)embeddedFiles[i].Data.Length));
            Assert.That(result.didx.LoadedMedia[i].Offset, Is.EqualTo(offset));
            
            // Check if offset is padded to 16 bytes
            Assert.That(result.didx.LoadedMedia[i].Offset % 16, Is.EqualTo(0));
            
            offset += PadTo16(result.didx.LoadedMedia[i].Size);
        }
    }
    
    [TestCase("ENVS_v134.bin", 134)]
    [TestCase("ENVS_v56.bin", 56)]
    public void EnvironmentSettings_Reserializes(string filename, int version)
    {
        var data = TestData.GetTestDataBytes(@"EnvironmentSettings", filename);
        var (_, result) = TestHelpers.Deserialize<ChunkContainer>(data, version);
        var envs = WwiseBankMapper.GetEnvironmentSettings(result.Chunk as EnvironmentSettingsChunk, (uint)version);

        var envsChunk = WwiseBankMapper.WriteEnvironmentSettings(envs, (uint)version);
        if (envsChunk is null) Assert.Fail("Reserialized EnvironmentSettingsChunk is null");
        
        var reserialized = TestHelpers.Serialize(new ChunkContainer(envsChunk!), version);
        Assert.That(reserialized, Is.EquivalentTo(data));
    }
    
    private uint PadTo16(uint position) => ((position + 15) / 16) * 16;
    
    private int PadTo16(int position) => ((position + 15) / 16) * 16;
}