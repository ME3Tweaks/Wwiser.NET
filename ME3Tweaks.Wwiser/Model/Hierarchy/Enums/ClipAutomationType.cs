using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class ClipAutomationType : IBinarySerializable
{
    public ClipAutomationTypeInner Value { get; set; }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var uintValue = (uint)Value;

        if (version < 112 && Value >= ClipAutomationTypeInner.FadeIn) uintValue--; // HPF is v112+ only
        
        stream.Write(BitConverter.GetBytes(uintValue));
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        Value = DeserializeStatic(stream, version);
    }

    public static ClipAutomationTypeInner DeserializeStatic(Stream stream, uint version)
    {
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();
        var initialValue = BitConverter.ToUInt32(span);

        if (version < 112 && initialValue >= 2) initialValue++;

        return (ClipAutomationTypeInner)initialValue;
    }
    
    public enum ClipAutomationTypeInner : uint
    {
        Volume = 0,
        LPF = 1,
        HPF = 2, // v112 >= only
        FadeIn = 3,
        FadeOut = 4
        
    }
}

