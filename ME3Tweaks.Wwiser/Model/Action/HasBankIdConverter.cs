using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.Action;

public class HasBankIdConverter : IValueConverter
{
    /// <summary>
    /// Returns true if an action of a given ActionType should have bank ID, false otherwise
    /// </summary>
    /// <param name="value">ActionType</param>
    /// <param name="parameter"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        if (value is not ActionType type)
        {
            throw new ArgumentException();
        }

        return type.Value switch
        {
            ActionTypeValue.Play or 
                ActionTypeValue.PlayAndContinue or 
                ActionTypeValue.PlayEventUnknown => true,
            _ => false
        };
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotImplementedException();
    }
}