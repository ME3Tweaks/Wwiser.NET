using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class Attenuation : HircItem
{
    [FieldOrder(0)]
    [SerializeAs(SerializedType.UInt1)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 136, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public bool IsHeightSpreadEnabled { get; set; }
    
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsConeEnabled { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhen(nameof(IsConeEnabled), true)]
    public ConeParams ConeParams { get; set; }
    
    [FieldOrder(3)]
    public CurveToUse CurveToUse { get; set; }
    
    //TODO: <=v36 this is a uint - not relevant to mass effect
    [FieldOrder(4)]
    public byte NumCurves { get; set; }
    
    [FieldOrder(5)]
    [FieldCount(nameof(NumCurves))]
    public List<RtpcConversionTable> Curves { get; set; }
    
    [FieldOrder(6)]
    public RtpcCurves RtpcCurves { get; set; }
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
    [SerializeWhen(nameof(BankSerializationContext.Version), 89, 
        ComparisonOperator.GreaterThan, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public float HighPass { get; set; }
}