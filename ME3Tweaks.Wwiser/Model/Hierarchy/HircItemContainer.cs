using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainer
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public required byte Type { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint Type32 {
        get => Type;
        set => Type = (byte)value;
    }
    
    /// <summary>
    /// Turns type byte into enum describing HIRC item type. Only applicable on versions &lt;128.
    /// This class does not know what version it is!
    /// </summary>
    [Ignore]
    public HircType HircType
    {
        get => (HircType)Type;
        set => Type = (byte)value;
    }
    
    /// <summary>
    /// Turns type byte into enum describing HIRC item type. Only applicable on versions &gt;=128.
    /// This class does not know what version it is!
    /// </summary>
    [Ignore]
    public HircType128 HircType128
    {
        get => (HircType128)Type;
        set => Type = (byte)value;
    }
    
    [FieldOrder(2)]
    public uint Size { get; init; }

    [FieldOrder(3)]
    [FieldLength(nameof(Size))]
    public HircItem Item;

}