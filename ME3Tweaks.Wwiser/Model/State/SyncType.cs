using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.State;


public class SyncType : IBinarySerializable
{
    [Ignore]
    public SyncTypeInner Value { get; set; }
    public enum SyncTypeInner : byte
    {
        Immediate,
        NextGrid,
        NextBar,
        NextBeat,
        NextMarker,
        NextUserMarker,
        EntryMarker,
        ExitMarker,
        ExitNever,
        LastExitPosition
    }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36) stream.Write(BitConverter.GetBytes((uint)Value));
        else stream.WriteByte((byte)Value);
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 36)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            Value = (SyncTypeInner)BitConverter.ToUInt32(span);
        }
        else
        {
            Value =  (SyncTypeInner)stream.ReadByte();
        }
    }
}
