using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class LayerContainer : HircItem, IHasNode
{
    [FieldOrder(0)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();
    
    [FieldOrder(1)] 
    public Children Children { get; set; } = new();

    [FieldOrder(2)] 
    public uint LayerCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(LayerCount))]
    public List<Layer> Layers { get; set; } = new();
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(118, ComparisonOperator.GreaterThan)]
    public bool IsContinuousValidation { get; set; }
}

public class Layer : AkIdentifiable
{
    [FieldOrder(0)] 
    public RtpcParameterNodeBase Rtpc { get; set; } = new();
    
    [FieldOrder(1)]
    public uint RtpcId { get; set; }

    [FieldOrder(2)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public RtpcType RtpcType { get; set; } = new();
    
    [FieldOrder(3)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public float CrossfadingRtpcDefaultValue { get; set; }
    
    [FieldOrder(4)]
    public uint AssociatedChildCount { get; set; }

    [FieldOrder(5)] 
    public List<AssociatedChild> AssociatedChildren { get; set; } = new();
}

public class AssociatedChild : AkIdentifiable
{
    [FieldOrder(0)]
    public uint CurveSize { get; set; }

    [FieldOrder(1)] 
    public List<RtpcGraphItem> Curves { get; set; } = new();
}