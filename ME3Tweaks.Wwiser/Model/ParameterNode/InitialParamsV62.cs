using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class InitialParamsV62
{
    // This uint could be a uni for some prop ids. It's dumb
    [FieldOrder(0)] 
    public PropBundle<SmartPropId, uint> Parameters { get; set; } = new();
    
    [FieldOrder(1)]
    public PropBundle<SmartPropId, UnionRange> RangedModifiers { get; set; } = new();
}