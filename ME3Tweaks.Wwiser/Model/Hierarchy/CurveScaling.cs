using System;
using System.IO;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class CurveScaling : IBinarySerializable
{
    public CurveScalingInner Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36)
        {
            stream.Write(BitConverter.GetBytes((uint)Value));
        }
        else
        {
            stream.WriteByte((byte)Value);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (CurveScalingInner)BitConverter.ToUInt32(span);
        }
        else
        {
            Value =  (CurveScalingInner)stream.ReadByte();
        }
    }

    public enum CurveScalingInner : sbyte
    {
        None,
        /// <summary>
        /// Version 46-62 only, unsupported on others
        /// </summary>
        dB_255,
        dB_96_3,
        /*Frequency_20_20000, // I think this is a version dependant enum but the values mean the exact same with updated la
        dB_96_3_NoCheck,*/
        Log,
        dBToLin
    }
}