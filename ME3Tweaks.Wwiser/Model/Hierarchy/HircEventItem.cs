using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class HircEventItem : HircItem
{
    [FieldOrder(0)]
    public BadVarCount ActionCount { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ActionCount), ConverterType = typeof(ShitValueConverter))]
    public required List<uint> ActionIds { get; set; }
}

public class ShitValueConverter : IValueConverter
{
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        return ((BadVarCount)value).Value;
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotImplementedException();
    }
}

