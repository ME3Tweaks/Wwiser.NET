using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateProp
{
    [FieldOrder(0)]
    public VarCount PropertyId { get; set; }
    
    [FieldOrder(1)]
    public AccumType AccumType { get; set; } 
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 126,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public bool InDb { get; set; }
}