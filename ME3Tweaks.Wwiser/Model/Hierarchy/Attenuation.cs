using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Attenuation : HircItem
{
    [FieldOrder(0)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhenVersion(136, ComparisonOperator.GreaterThan)]
    public bool IsHeightSpreadEnabled { get; set; }
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsConeEnabled { get; set; }

    [FieldOrder(2)]
    [SerializeWhen(nameof(IsConeEnabled), true)]
    public ConeParams ConeParams { get; set; } = new();

    [FieldOrder(3)] 
    public CurveToUse CurveToUse { get; set; } = new();
    
    [FieldOrder(4)] 
    public V36Count NumCurves { get; set; } = new();

    [FieldOrder(5)]
    [FieldCount($"{nameof(NumCurves)}.{nameof(NumCurves.Value)}")]
    public List<RtpcConversionTable> Curves { get; set; } = new();

    [FieldOrder(6)] 
    public RtpcParameterNodeBase RtpcParameterNodeBase { get; set; } = new();
}

public class CurveToUse : IBinarySerializable
{
    [Ignore]
    public sbyte[] CurveMap { get; set; } = new sbyte[19];

    private static int GetCurveCount(uint version) => version switch
    {
        <= 62 => 5,
        <= 72 => 4,
        <= 89 => 5,
        <= 141 => 7,
        _ => 19
    };

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        for(int i = 0; i < GetCurveCount(version); i++)
        {
            stream.WriteByte((byte)CurveMap[i]);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        Array.Fill(CurveMap, (sbyte)-1);
        for(int i = 0; i < GetCurveCount(version); i++)
        {
            CurveMap[i] = (sbyte)stream.ReadByte();
        }
    }
}

public class ConeParams
{
    [FieldOrder(0)]
    public float InsideDegrees { get; set; }
    
    [FieldOrder(1)]
    public float OutsideDegrees { get; set; }
    
    [FieldOrder(3)]
    public float OutsideVolume { get; set; }
    
    [FieldOrder(4)]
    public float LowPass { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public float HighPass { get; set; }
}