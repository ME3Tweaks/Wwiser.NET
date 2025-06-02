using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.GlobalSettings;

public class STMGStateGroup : AkIdentifiable
{
    [FieldOrder(1)]
    public uint DefaultTransitionTime { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public uint CustomStateCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(CustomStateCount))]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public List<CustomState> CustomStates { get; set; } = new();
    
    [FieldOrder(4)]
    public uint StateTransitionCount { get; set; }
    
    [FieldOrder(5)]
    [FieldCount(nameof(StateTransitionCount))]
    public List<StateTransition> StateTransitions { get; set; } = new();
}

public class CustomState
{
    [FieldOrder(0)]
    public uint StateId { get; set; } // StateType?

    [FieldOrder(1)] 
    public HircItemContainer State { get; set; } = new HircItemContainer
    {
        Type = new HircSmartType() { Value = HircType.State },
        Item = new Hierarchy.State()
    };
}

public class StateTransition
{
    [FieldOrder(0)]
    public uint StateFromId { get; set; }
    
    [FieldOrder(1)]
    public uint StateToId { get; set; }
    
    [FieldOrder(2)]
    public uint TransitionTime { get; set; }
}