using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.SerializationHelpers;

public class HasPluginParamConverter : IValueConverter
{
    /// <summary>
    /// Returns true if BankSourceData should have plugin parameters, false otherwise
    /// </summary>
    /// <param name="value">Plugin object</param>
    /// <param name="parameter"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        if (value is not Plugin)
        {
            throw new ArgumentException();
        }

        var plugin = value as Plugin;
        var version = context.FindAncestor<BankSerializationContext>().Version;
        var pluginType = plugin?.PluginId & 0x0f;

        return version switch
        {
            <= 26 => true,
            <= 126 => pluginType is 2 or 5,
            _ => pluginType is 2
        };
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotImplementedException();
    }
}