using BinarySerialization;

namespace ME3Tweaks.Wwiser.SerializationHelpers;

public class BetweenConverter : IValueConverter
{
    /// <returns>True if value is between the given int tuple parameter, inclusive</returns>
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        if(parameter is not int[] { Length: 2 })
            throw new ArgumentException($"Should be array", nameof(parameter));

        var numericValue = System.Convert.ToInt32(value);
        var bounds = parameter as int[] ?? new[] { 0, 0 };

        return (bounds[0] <= numericValue) && (numericValue <= bounds[1]);
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotSupportedException();
    }
}