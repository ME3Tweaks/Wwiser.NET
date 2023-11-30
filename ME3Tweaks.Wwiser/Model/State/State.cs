using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.State;

public class State : AkIdentifiable
{
    // Lower versions - reference to something else?
    [SerializeWhen(nameof(BankSerializationContext.Version), 145,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint StateInstanceId { get; set; }
    
    // Higher versions, data is inlined???? idk
    [SerializeWhen(nameof(BankSerializationContext.Version), 145,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public PropBundle_Float_UnsignedShort Properties { get; set; }
    
}