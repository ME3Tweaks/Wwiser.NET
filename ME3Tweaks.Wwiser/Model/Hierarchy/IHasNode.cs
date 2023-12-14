using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public interface IHasNode
{
    public NodeBaseParameters NodeBaseParameters { get; set; }
}