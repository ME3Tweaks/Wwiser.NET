using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class StringMappingChunk : Chunk
{
    public override string Tag => @"STID";
    // TODO: Totally different parsing for <= v26. A different subclass?

    [FieldOrder(0)] 
    public AKBKStringType StringType { get; set; }

    [FieldOrder(1)] 
    public uint StringCount { get; set; }

    [FieldOrder(3)]
    [FieldCount(nameof(StringCount))]
    public List<BankHashHeader> BankIdToFilename { get; set; }
}

public enum AKBKStringType : uint
{
    None = 0x0,
    Bank = 0x1
}

public class BankHashHeader
{
    public BankHashHeader(uint bankId, string fileName)
    {
        BankId = bankId;
        FileName = fileName;
        StringLength = (byte)FileName.Length;
    }

    [FieldOrder(0)]
    public uint BankId { get; set; }

    [FieldOrder(1)]
    public byte StringLength { get; set; }

    [FieldOrder(3)]
    [FieldLength(nameof(StringLength))]
    public string FileName { get; set; }
}