using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainer : IHircItemContainer
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public required HircType Type { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint Type32
    {
        get => (uint)Type;
        set => Type = (HircType)value;
    }
    
    [FieldOrder(2)]
    public uint Size { get; set; }

    [FieldOrder(3)]
    [FieldLength(nameof(Size))]
    [SubtypeFactory(nameof(Type), typeof(HircTypeFactory))]
    public required HircItem Item { get; set; }
}