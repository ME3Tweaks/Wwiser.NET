using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class NodeBaseParameters
{
    [FieldOrder(0)]
    public InitialFxParams FxParams { get; set; }
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), true, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext,
        ConverterType = typeof(BetweenConverter), 
        ConverterParameter = new[] {90, 145})]
    public bool OverrideAttachmentParams { get; set; }
    
    [FieldOrder(2)]
    public uint OverrideBusId { get; set; }
    
    [FieldOrder(3)]
    public uint DirectParentId { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public byte Priority { get; set; }
    
    [FieldOrder(5)]
    public PriorityOverrideFlags PriorityOverrideFlags { get; set; }

    [FieldOrder(6)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public sbyte DistOffset { get; set; }
    
    // TODO: Convert between these two variants when changing version
    [FieldOrder(7)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public InitialParamsV56 InitialParams56 { get; set; }
    
    [FieldOrder(8)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 56, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public InitialParamsV62 InitialParams62 { get; set; }
    
    [FieldOrder(9)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 52, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint StateGroupId { get; set; }
    
    [FieldOrder(10)]
    public Positioning.PositioningChunk PositioningChunk { get; set; }
    
    [FieldOrder(11)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 65, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public AuxParams AuxParams { get; set; }
    
}