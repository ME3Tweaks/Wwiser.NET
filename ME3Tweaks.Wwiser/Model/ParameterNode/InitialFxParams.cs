using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class InitialFxParams
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
    public List<FxChunk> FxChunks { get; set; } = new();
}