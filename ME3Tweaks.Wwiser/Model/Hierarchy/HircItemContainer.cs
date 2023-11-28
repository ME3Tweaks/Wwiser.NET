using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircItemContainer : IHircItemContainer
{
    [FieldOrder(0)]
    public required HircSmartType SmartType { get; set; }
    
    [FieldOrder(1)]
    public uint Size { get; set; }

    [FieldOrder(3)]
    [FieldLength(nameof(Size))]
    [SubtypeFactory($"{nameof(SmartType)}.{nameof(SmartType.Value)}", typeof(HircTypeFactory))]
    public required HircItem Item { get; set; }
}