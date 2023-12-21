using System;
using System.IO;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.RTPC;

public class RtpcType : IBinarySerializable
{
    public RtpcTypeInner Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 140)
        {
            if (Value is > RtpcTypeInner.MIDIParameter and < RtpcTypeInner.Modulator)
            {
                throw new NotSupportedException();
            }
            
            if (Value is RtpcTypeInner.Modulator)
            {
                stream.WriteByte(0x02);
            }
            else
            {
                stream.WriteByte((byte)Value);
            }
        }
        else
        {
            stream.WriteByte((byte)Value);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var initialValue = stream.ReadByte();
        if (version <= 140 && initialValue == 0x02)
        {
            initialValue = 0x04;
        }
        Value = (RtpcTypeInner)initialValue;
    }

    public RtpcType() { }

    public RtpcType(RtpcTypeInner type)
    {
        Value = type;
    }
    
    public enum RtpcTypeInner : byte 
    {
        GameParameter,
        MIDIParameter,
        Switch,
        State,
        Modulator
    }
}