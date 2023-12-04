using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class NodeBaseParameters
{
    [FieldOrder(0)]
    public InitialFxParams FxParams { get; set; } = new();
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersionBetween(90, 145)]
    public bool OverrideAttachmentParams { get; set; }
    
    [FieldOrder(2)]
    public uint OverrideBusId { get; set; }
    
    [FieldOrder(3)]
    public uint DirectParentId { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public byte Priority { get; set; }
    
    [FieldOrder(5)]
    public PriorityOverrideFlags PriorityOverrideFlags { get; set; } = new();

    [FieldOrder(6)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public sbyte DistOffset { get; set; }
    
    // TODO: Convert between these two variants when changing version
    [FieldOrder(7)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public InitialParamsV56 InitialParams56 { get; set; } = new();
    
    [FieldOrder(8)]
    [SerializeWhenVersion(56, ComparisonOperator.GreaterThan)]
    public InitialParamsV62 InitialParams62 { get; set; } = new();
    
    [FieldOrder(9)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public uint StateGroupId { get; set; }

    [FieldOrder(10)] 
    public Positioning.PositioningChunk PositioningChunk { get; set; } = new();
    
    [FieldOrder(11)]
    [SerializeWhenVersion(65, ComparisonOperator.GreaterThan)]
    public AuxParams AuxParams { get; set; } = new();
    
}