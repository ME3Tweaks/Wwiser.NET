using BinarySerialization;
using Wwiser.NET.Model;

namespace Wwiser.NET.Tests;

public class BankHeaderTests
{

    [Test]
    public void V134_Parses()
    {
        var data = new byte[] { 0x42, 0x4B, 0x48, 0x44, 0x14, 0x00, 0x00, 0x00, 0x86, 0x00, 0x00, 0x00, 0x05, 0xE8, 0x22, 0x55, 0x3E, 0x5D, 0x70, 0x17, 0x00, 0x00, 0x00, 0x00, 0x94, 0x1D, 0x00, 0x00 };
        var serializer = new BinarySerializer();
        var resolvedClass = serializer.Deserialize(data, typeof(ChunkContainer));
        var result = resolvedClass as ChunkContainer;
        Assert.AreEqual("BKHD", result.Tag);
        Assert.IsInstanceOf<BankHeader>(result.Chunk);
        var chunk = result.Chunk as BankHeader;
        Assert.AreEqual(134, chunk.BankGeneratorVersion);
        Assert.AreEqual(7572, chunk.ProjectID);
    }
}