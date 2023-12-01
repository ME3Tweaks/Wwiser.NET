using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Sound : HircItem
{
    [FieldOrder(0)]
    public BankSourceData BankSourceData { get; set; }
    
    [FieldOrder(1)]
    public NodeBaseParameters NodeBaseParameters { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public short Loop { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public short LoopModMin { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public short LoopModMax { get; set; }
}