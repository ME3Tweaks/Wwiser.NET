using BinarySerialization;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class ActorMixer : HircItem
{
    [FieldOrder(0)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();
    
    [FieldOrder(1)]
    public uint ChildrenCount { get; set; }

    [FieldOrder(2)]
    [FieldCount(nameof(ChildrenCount))]
    public List<uint> Children { get; set; } = new();
}