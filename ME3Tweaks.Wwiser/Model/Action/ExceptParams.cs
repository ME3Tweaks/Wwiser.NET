using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ExceptParams
{
    [FieldOrder(0)] 
    public VarCount ExceptionCount { get; set; } = new();

    [FieldOrder(1)]
    [FieldCount($"{nameof(ExceptionCount)}.{nameof(ExceptionCount.Value)}")]
    public List<ElementException> Exceptions { get; set; } = new();
}

public class ElementException : AkIdentifiable
{
    [FieldOrder(0)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(65, ComparisonOperator.GreaterThan)]
    public bool IsBus { get; set; }
}