using BinarySerialization;

namespace ME3Tweaks.Wwiser.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class SerializeWhenVersionAttribute : SerializeWhenAttribute
{
    public SerializeWhenVersionAttribute(object value, ComparisonOperator @operator = ComparisonOperator.Equal) : base(nameof(BankSerializationContext.Version), value, @operator)
    {
        RelativeSourceMode = RelativeSourceMode.SerializationContext;
    }
}