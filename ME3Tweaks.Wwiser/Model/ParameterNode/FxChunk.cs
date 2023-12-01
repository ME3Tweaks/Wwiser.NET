using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class FxChunk : IAkIdentifiable
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 26, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public byte FxIndex { get; set; }
    
    [FieldOrder(1)]
    public uint Id { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 26, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public byte BitsFxBypass { get; set; }
    
    [FieldOrder(3)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), true, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext,
        ConverterType = typeof(BetweenConverter), 
        ConverterParameter = new[] {49, 145})]
    public bool IsShareSet { get; set; }
    
    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 145, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public bool IsRendered { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public PluginParameters PluginParameters { get; set; }
    
    //TODO: Custom serialized class - merge with IsShareSet, IsRendered, BitsFXBypass
    [FieldOrder(6)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 145, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public byte BitVector { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint NumBankData { get; set; }
    
    [FieldOrder(8)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 48, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public List<BankData> BankDatas { get; set; }

    public class BankData
    {
        [FieldOrder(0)]
        public uint FxParameterSetId { get; set; }
        
        [FieldOrder(1)]
        public uint Item { get; set; }
    }
}