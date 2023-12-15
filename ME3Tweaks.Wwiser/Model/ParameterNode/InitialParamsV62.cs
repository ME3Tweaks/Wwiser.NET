using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class InitialParamsV62
{
    [FieldOrder(0)] 
    public byte ParamLength { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(ParamLength))]
    public List<SmartPropId> ParameterIds { get; set; } = new();
    
    [FieldOrder(2)]
    [FieldCount(nameof(ParamLength), BindingMode = BindingMode.OneWay)]
    public List<ParameterValue> ParameterValues { get; set; } = new();
    
    [FieldOrder(3)] 
    public byte RangeLength { get; set; }

    [FieldOrder(4)]
    [FieldCount(nameof(RangeLength))]
    public List<SmartPropId> RangeIds { get; set; } = new();
    
    [FieldOrder(5)]
    [FieldCount(nameof(RangeLength), BindingMode = BindingMode.OneWay)]
    public List<UnionRange> RangeValues { get; set; } = new();

    public void AddParameter(PropId id, ParameterValue value)
    {
        ParameterIds.Add(new SmartPropId() { PropValue = id });
        ParameterValues.Add(value);
    }
    
    public void AddRange(PropId id, Uni low, Uni high)
    {
        RangeIds.Add(new SmartPropId() { PropValue = id });
        RangeValues.Add(new UnionRange(low, high));
    }
    
    public class ParameterValue : Uni
    {
        public ParameterValue() : base() { }

        public ParameterValue(Uni i)
        {
            Float = i.Float;
            Integer = i.Integer;
        }
        
        public new void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
        {
            var initialParams = serializationContext.FindAncestor<InitialParamsV62>();
            var id = initialParams.ParameterIds[initialParams.ParameterValues.Count + 1];
            if (id.PropValue is PropId.AttachedPluginFXID /*or PropId.AttenuationID*/)
            {
                stream.Write(BitConverter.GetBytes(Integer));
            }
            else
            {
                base.Serialize(stream, endianness, serializationContext);
            }
        }

        public new void Deserialize(Stream stream, Endianness endianness,
            BinarySerializationContext serializationContext)
        {
            var initialParams = serializationContext.FindAncestor<InitialParamsV62>();
            var id = initialParams.ParameterIds[initialParams.ParameterValues.Count + 1];
            if (id.PropValue is PropId.AttachedPluginFXID /*or PropId.AttenuationID*/)
            {
                Span<byte> span = stackalloc byte[4];
                var read = stream.Read(span);
                if (read != 4) throw new Exception();
                Integer = BitConverter.ToUInt32(span);
            }
            else
            {
                base.Deserialize(stream, endianness, serializationContext);
            }
        }
    }
}