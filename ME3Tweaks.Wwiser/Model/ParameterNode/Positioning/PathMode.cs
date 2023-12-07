using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class PathMode : IBinarySerializable
{
    public PathModeInner Value { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 89)
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
        if (version <= 89)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (PathModeInner)BitConverter.ToUInt32(span);
        }
        else
        {
            Value =  (PathModeInner)stream.ReadByte();
        }
    }

    public enum PathModeInner : sbyte
    {
        StepSequence,
        StepRandom,
        ContinuousSequence,
        ContinuousRandom,
        StepSequencePickNewPath,
        StepRandomPickNewPath
    }
}