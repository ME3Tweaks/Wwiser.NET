using System.Reflection.Metadata.Ecma335;
using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

// These are exactly the same
public class FxShareSet : FxBase { }
public class FxCustom : FxBase { }

public class FxBase : HircItem
{
    [FieldOrder(0)]
    public required Plugin Plugin { get; set; }
    
    [FieldOrder(1)]
    public uint ParamSize { get; set; }

    [FieldOrder(2)]
    [FieldLength(nameof(ParamSize))]
    [SubtypeDefault(typeof(EmptyPluginParams))]
    public required IPluginParams PluginParams { get; set; } = new EmptyPluginParams();
    
    // Turn into class - AKMediaMap???
    [FieldOrder(3)]
    public byte BankDataCount { get; set; }
    
    [FieldOrder(4)]
    [FieldCount(nameof(BankDataCount))]
    public List<MediaMapItem> Media { get; set; }
    
    [FieldOrder(5)]
    public RtpcCurves RtpcCurves { get; set; }
    
    [FieldOrder(6)]
    [SerializeWhen(nameof(BankSerializationContext.Version), true, 
        RelativeSourceMode = RelativeSourceMode.SerializationContext,
        ConverterType = typeof(BetweenConverter), 
        ConverterParameter = new[] {123, 126})]
    public ushort Unk1 { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 126,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public StateChunk_Aware StateChunk { get; set; }
    
    [FieldOrder(8)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 90,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public ushort RtpcInitCount { get; set; }
    
    [FieldOrder(8)]
    [FieldCount(nameof(RtpcInitCount))]
    [SerializeWhen(nameof(BankSerializationContext.Version), 90,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public List<RtpcInitValue> RtpcInitValues { get; set; }
    
    
    public class RtpcInitValue
    {
        [FieldOrder(0)]
        public BadVarCount ParameterId { get; set; }
        
        [FieldOrder(1)]
        [SerializeWhen(nameof(BankSerializationContext.Version), 126,
            ComparisonOperator.GreaterThan,
            RelativeSourceMode = RelativeSourceMode.SerializationContext)]
        public byte RtpcAccum { get; set; }
        
        [FieldOrder(2)]
        public float InitValue { get; set; }
    }
}

public class MediaMapItem
{
    [FieldOrder(0)]
    public byte Index { get; set; }
    
    [FieldOrder(1)]
    public uint SourceId { get; set;  }
}

