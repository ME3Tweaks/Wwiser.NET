using BinarySerialization;
using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser;

/// <summary>
/// Maps between <see cref="WwiseBank"/> and <see cref="WwiseBankRoot"/>.
/// </summary>
internal class WwiseBankMapper
{
    internal WwiseBankRoot MapToRoot(WwiseBank bank)
    {
        // Convert embedded files to DATA and DIDX chunks, set padding accordingly
        var (data, didx) = WriteEmbeddedFiles(bank.EmbeddedFiles);
        if (data is not null && didx is not null) SetBankHeaderPadding(bank.BKHD, didx);
        
        var chunks = new List<Chunk?> 
            {
                bank.BKHD, didx, data, bank.HIRC, bank.STID, bank.STMG, bank.PLAT, bank.INIT
            }.Where(x => x is not null)
            .Select(x => x!)
            .Where(x => x.IsAllowedInVersion(bank.BKHD.BankGeneratorVersion))
            .Select(x => new ChunkContainer(x))
            .ToArray();

        return new WwiseBankRoot()
        {
            Chunks = chunks
        };
    }

    internal WwiseBank MapToBank(WwiseBankRoot root)
    {
        WwiseBank bank = new();
        DataChunk? DATA = null;
        MediaIndexChunk? DIDX = null;
        foreach (var chunk in root.Chunks)
        {
            switch (chunk.Tag)
            {
                case "BKHD":
                    bank.BKHD = (chunk.Chunk as BankHeaderChunk)!;
                    break;
                case "DIDX":
                    DIDX = chunk.Chunk as MediaIndexChunk;
                    break;
                case "DATA":
                    DATA = chunk.Chunk as DataChunk;
                    break;
                case "HIRC":
                    bank.HIRC = chunk.Chunk as HierarchyChunk;
                    break;
                case "STID":
                    bank.STID = chunk.Chunk as StringMappingChunk;
                    break;
                case "STMG":
                    bank.STMG = chunk.Chunk as GlobalSettingsChunk;
                    break;
                case "PLAT":
                    bank.PLAT = chunk.Chunk as PlatformChunk;
                    break;
                case "INIT":
                    bank.INIT = chunk.Chunk as PluginChunk;
                    break;
            }
        }

        // Convert DATA and DIDX chunks to embedded files
        if (DATA is not null && DIDX is not null)
        {
            bank.EmbeddedFiles = GetEmbeddedFiles(DATA, DIDX);
        }

        return bank;
    }

    internal List<EmbeddedFile> GetEmbeddedFiles(DataChunk DATA, MediaIndexChunk DIDX)
    {
        var embeddedFiles = new List<EmbeddedFile>();
        var dataStream = new MemoryStream(DATA.Data);
        foreach (var media in DIDX.LoadedMedia.OrderBy(m => m.Offset))
        {
            var embeddedFile = new EmbeddedFile
            {
                Id = media.Id,
                Data = new byte[media.Size]
            };
            dataStream.Seek(media.Offset, SeekOrigin.Begin);
            dataStream.ReadExactly(embeddedFile.Data, 0, (int)media.Size);
            embeddedFiles.Add(embeddedFile);
        }
        return embeddedFiles;
    }

    internal (DataChunk? data, MediaIndexChunk? didx) WriteEmbeddedFiles(List<EmbeddedFile> embeddedFiles)
    {
        if(embeddedFiles.Count == 0) return (null, null);

        if (embeddedFiles.Select(x => x.Id).Distinct().Count() < embeddedFiles.Count)
        {
            throw new InvalidOperationException("Embedded files must have unique IDs.");
        }
        
        var dataStream = new MemoryStream(embeddedFiles.Sum(f => f.Data.Length));
        var didxChunk = new MediaIndexChunk();

        for(var i = 0; i < embeddedFiles.Count; i++)
        {
            var file = embeddedFiles[i];
            
            didxChunk.LoadedMedia.Add(new MediaHeader
            {
                Id = file.Id,
                Offset = (uint)dataStream.Position,
                Size = (uint)file.Data.Length
            });
            
            dataStream.Write(file.Data, 0, file.Data.Length);
            
            if(dataStream.Position % 16 != 0 && i < embeddedFiles.Count - 1)
            {
                // Pad to 16-byte alignment if there is a next file
                var padding = new byte[16 - (dataStream.Position % 16)];
                dataStream.Write(padding, 0, padding.Length);
            }
        }

        var dataChunk = new DataChunk
        {
            Data = dataStream.ToArray()
        };
        
        return(dataChunk, didxChunk);
    }

    internal void SetBankHeaderPadding(BankHeaderChunk bkhd, MediaIndexChunk? didx)
    {
        var serializer = new BinarySerializer();
        var context = new BankSerializationContext(bkhd.BankGeneratorVersion, false, bkhd.FeedbackInBank);
        bkhd.Padding = new BankHeaderPadding();
            
        var bkhdSize= serializer.SizeOf(new ChunkContainer(bkhd), context);
        var didxSize = (didx != null) ? 
            serializer.SizeOf(new ChunkContainer(didx), context) : 0;

        var dataOffset = bkhdSize + didxSize;
        bkhd.Padding.SetPadding(dataOffset);
    }
}