using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;


public class NodeBaseParameters
{
    [FieldOrder(0)]
    public ParameterNode_InitialFxParams FxParams { get; set; }
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), true, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext,
        ConverterType = typeof(BetweenConverter), 
        ConverterParameter = new[] {90, 145})]
    public bool OverrideAttachmentParams { get; set; }
    
    [FieldOrder(2)]
    public uint OverrideBusId { get; set; }
    
    [FieldOrder(3)]
    public uint DirectParentId { get; set; }
    
}

public class ParameterNode_InitialFxParams
{
    [FieldOrder(0)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsOverrideParentFx { get; set; }
    
    //TODO: <=v26 this is a bool serialized as a uint - not relevant to mass effect
    [FieldOrder(1)]
    public byte NumFx { get; set; }
    
    //TODO: This doesn't exist <=26 and means something else >145 - not relevant to mass effect
    [FieldOrder(2)]
    [SerializeWhen(nameof(NumFx), 0, ComparisonOperator.GreaterThan)]
    public byte BitsFxBypass { get; set; }
    
    [FieldOrder(3)]
    [FieldCount(nameof(NumFx))]
    public List<FxChunk> FxChunks { get; set; }
}

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