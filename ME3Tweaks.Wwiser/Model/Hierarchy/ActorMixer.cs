using BinarySerialization;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class ActorMixer : HircItem, IHasNode
{
    [FieldOrder(0)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();

    [FieldOrder(1)] 
    public Children Children { get; set; } = new();
}