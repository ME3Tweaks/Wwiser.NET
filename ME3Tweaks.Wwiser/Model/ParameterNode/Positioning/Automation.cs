using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class Automation
{
    [FieldOrder(0)] 
    public PathMode PathMode { get; set; } = new();
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(89, ComparisonOperator.LessThanOrEqual)]
    public bool IsLooping { get; set; }
    
    [FieldOrder(2)]
    public int TransitionTime { get; set; }
    
    [FieldOrder(3)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersionBetween(37, 89)]
    public bool FollowOrientation { get; set; }
    
    [FieldOrder(4)]
    public uint NumVertices { get; set; }

    [FieldOrder(5)]
    [FieldCount(nameof(NumVertices))]
    public List<PathVertex> Vertices { get; set; } = new();
    
    [FieldOrder(6)]
    public uint NumPathListItem { get; set; }

    [FieldOrder(7)]
    [FieldCount(nameof(NumPathListItem))]
    public List<PathListItemOffset> PathList { get; set; } = new();

    [FieldOrder(8)]
    [SerializeWhenVersion(36, ComparisonOperator.GreaterThan)]
    [FieldCount(nameof(NumPathListItem), BindingMode = BindingMode.OneWay)]
    public List<AutomationParams3D> AutomationParams { get; set; } = new();
}

public class PathVertex
{
    [FieldOrder(0)] 
    public float X { get; set; }
    
    [FieldOrder(1)] 
    public float Y { get; set; }
    
    [FieldOrder(2)] 
    public float Z { get; set; }
    
    [FieldOrder(3)] 
    public float Duration { get; set; }
}

public class PathListItemOffset
{
    [FieldOrder(0)]
    public uint VerticesOffset { get; set; }
    
    [FieldOrder(1)]
    public uint NumVertices { get; set; }
}

public class AutomationParams3D
{
    [FieldOrder(0)]
    public float XRange { get; set; }

    [FieldOrder(1)]
    public float YRange { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public float ZRange { get; set; }
}