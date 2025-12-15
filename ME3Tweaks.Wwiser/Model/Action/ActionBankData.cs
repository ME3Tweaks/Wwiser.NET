using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActionBankData
{
    [FieldOrder(1)]
    [SerializeWhenVersion(26, ComparisonOperator.GreaterThan)]
    public uint BankId { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(144, ComparisonOperator.GreaterThanOrEqual)]
    public uint BankType { get; set; }
}