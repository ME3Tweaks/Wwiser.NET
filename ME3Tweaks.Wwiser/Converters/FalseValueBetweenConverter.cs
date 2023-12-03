using System;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Converters;

public class FalseValueBetweenConverter : IValueConverter
{
    /// <returns>Same as between converter, but only serializes if value resolves to false</returns>
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        var version = context.FindAncestor<BankSerializationContext>().Version;
        if(parameter is not int[] { Length: 2 })
            throw new ArgumentException($"Should be array", nameof(parameter));
        var bounds = parameter as int[] ?? new[] { 0, 0 };
        
        if(value is not bool b)
            throw new ArgumentException($"Should be bool", nameof(value));
        
        return !b && (bounds[0] <= version) && (version <= bounds[1]);
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotSupportedException();
    }
}