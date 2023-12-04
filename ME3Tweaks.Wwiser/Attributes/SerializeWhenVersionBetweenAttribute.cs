using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;

namespace ME3Tweaks.Wwiser.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class SerializeWhenVersionBetweenAttribute : SerializeWhenVersionAttribute
{
    public SerializeWhenVersionBetweenAttribute(uint low, uint high, ComparisonOperator @operator = ComparisonOperator.Equal) : base(true, @operator)
    {
        RelativeSourceMode = RelativeSourceMode.SerializationContext;
        ConverterType = typeof(BetweenConverter);
        ConverterParameter = new[] { low, high };
    }
}