using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class State : AkIdentifiable
{
    [FieldOrder(0)]
    [SerializeWhenVersion(120, ComparisonOperator.LessThanOrEqual)]
    public uint StateId { get; set; }
    
    [FieldOrder(1)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsCustom { get; set; }
    
    // Lower versions - reference to something else?
    [FieldOrder(2)]
    [SerializeWhenVersion(145, ComparisonOperator.LessThanOrEqual)]
    public uint StateInstanceId { get; set; }
    
    // Higher versions, data is inlined???? idk
    [FieldOrder(3)]
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public ushort PropCount { get; set; }
    
    [FieldOrder(4)]
    [FieldCount(nameof(PropCount))]
    [SerializeWhenVersion(145, ComparisonOperator.GreaterThan)]
    public List<PropertyPair> Properties { get; set; } = new();
    
    public class PropertyPair
    {
        [FieldOrder(0)]
        public ushort Id { get; set; }
        
        [FieldOrder(1)]
        public float Value { get; set; }
    }
}