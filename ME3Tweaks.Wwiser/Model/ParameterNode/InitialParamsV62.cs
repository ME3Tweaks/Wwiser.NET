using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class InitialParamsV62
{
    [FieldOrder(0)] 
    public byte ParamLength { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ParamLength))]
    public List<ParameterPair> Parameters { get; set; } = new();
    
    [FieldOrder(3)] 
    public byte RangeLength { get; set; }

    [FieldOrder(4)]
    [FieldCount(nameof(RangeLength))]
    public List<RangePair> Ranges { get; set; } = new();
    
    public class ParameterPair
    {
        [FieldOrder(0)] 
        public SmartPropId Id { get; set; } = new();
        
        // This uint could be a uni for some prop ids. It's dumb
        [FieldOrder(1)]
        public uint Value { get; set; }
    }

    public class RangePair
    {
        [FieldOrder(0)] 
        public SmartPropId Id { get; set; } = new();
        
        [FieldOrder(1)]
        public UnionRange Value { get; set; }
    }
}