using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Action;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Action : HircItem
{
    [FieldOrder(0)] 
    public ActionType Type { get; set; } = new();
    
    [FieldOrder(1)]
    public uint TargetId { get; set; }

    [FieldOrder(2)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public RangedParameterInt Delay { get; set; } = new();
    
    [FieldOrder(3)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public uint SubSectionSize { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(65, ComparisonOperator.GreaterThan)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsBus { get; set; }

    [FieldOrder(5)]
    [SerializeWhenVersion(56, ComparisonOperator.GreaterThan)]
    public InitialParamsV62 PropBundle { get; set; } = new();

    [FieldOrder(6)]
    [FieldLength(nameof(SubSectionSize), BindingMode = BindingMode.OneWayToSource)]
    [SubtypeFactory($"{nameof(Type)}.{nameof(Type.Value)}", typeof(ActionParamsFactory),
        BindingMode = BindingMode.OneWay)]
    public required IActionParams ActionParams { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhen(nameof(Type), true, 
        ConverterType = typeof(HasBankIdConverter))]
    public ActionBankData BankData { get; set; } = new();

    [Ignore]
    public override HircType HircType =>  HircType.Action;
}