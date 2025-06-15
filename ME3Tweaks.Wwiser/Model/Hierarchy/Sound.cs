using BinarySerialization;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Sound : HircItem, IHasNode
{
    [FieldOrder(0)]
    public BankSourceData BankSourceData { get; set; } = new();
    
    [FieldOrder(1)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();
    
    [FieldOrder(2)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short Loop { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short LoopModMin { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public short LoopModMax { get; set; }
}