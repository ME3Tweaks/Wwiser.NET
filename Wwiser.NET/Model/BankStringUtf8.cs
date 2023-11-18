using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model;

public class BankStringUtf8
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 136,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public UInt32 Length;
    
    [FieldOrder(1)]
    [FieldLength(nameof(Length))]
    public string Value { get; set; }

    public BankStringUtf8(string value)
    {
        Value = value;
    }

    public static implicit operator string(BankStringUtf8 b) => b.Value;
}