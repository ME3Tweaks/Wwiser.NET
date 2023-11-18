using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

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