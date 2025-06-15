using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Plugins;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class FxChunk : IAkIdentifiable
{
    [FieldOrder(0)]
    [SerializeWhenVersion(26, ComparisonOperator.GreaterThan)]
    public byte FxIndex { get; set; }
    
    [FieldOrder(1)]
    public uint Id { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(26, ComparisonOperator.LessThanOrEqual)]
    public byte BitsFxBypass { get; set; }
    
    [FieldOrder(3)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersionBetween(49, 145)]
    public bool IsShareSet { get; set; }
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(145, ComparisonOperator.LessThanOrEqual)]
    public bool IsRendered { get; set; }

    [FieldOrder(5)]
    [SerializeWhenVersion(48, ComparisonOperator.LessThanOrEqual)]
    public PluginParameters PluginParameters { get; set; } = new();
    
    //TODO: Custom serialized class - merge with IsShareSet, IsRendered, BitsFXBypass
    [FieldOrder(6)]
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public byte BitVector { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhenVersion(48)]
    public uint NumBankData { get; set; }

    [FieldOrder(8)]
    [SerializeWhenVersion(48)]
    public List<BankDataItem> BankData { get; set; } = new();

    public class BankDataItem
    {
        [FieldOrder(0)]
        public uint FxParameterSetId { get; set; }
        
        [FieldOrder(1)]
        public uint Item { get; set; }
    }
}