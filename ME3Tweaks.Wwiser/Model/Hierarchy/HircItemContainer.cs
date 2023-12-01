using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainer
{
    [FieldOrder(0)]
    public required HircSmartType Type { get; set; }
    
    [FieldOrder(1)]
    public uint Size { get; set; }

    [FieldOrder(3)]
    [FieldLength(nameof(Size))]
    [SubtypeFactory($"{nameof(Type)}.{nameof(Type.Value)}", typeof(HircTypeFactory))]
    public required HircItem Item { get; set; }
}