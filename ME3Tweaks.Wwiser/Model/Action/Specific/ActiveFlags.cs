using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;


public class ActiveFlags : IBinarySerializable
{
    [Ignore]
    public bool IncludePendingResume { get; set; }
    
    [Ignore]
    public bool ApplyToStateTransitions { get; set; }
    
    [Ignore]
    public bool ApplyToDynamicSequence { get; set; }
    
    
    [Flags]
    public enum ActiveFlagsInner : byte
    {
        IncludePendingResume = 1 << 0,
        ApplyToStateTransitions = 1 << 1,
        ApplyToDynamicSequence = 1 << 2,
    }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 56)
        {
            stream.WriteBoolByte(IncludePendingResume);
            stream.WriteBoolByte(ApplyToStateTransitions);
            stream.WriteBoolByte(ApplyToDynamicSequence);
        }
        else if (version <= 62)
        {
            // TODO: Not sure
        }
        else
        {
            ActiveFlagsInner flags = 0;
            if (IncludePendingResume) flags |= ActiveFlagsInner.IncludePendingResume;
            if (ApplyToStateTransitions) flags |= ActiveFlagsInner.ApplyToStateTransitions;
            if (ApplyToDynamicSequence) flags |= ActiveFlagsInner.ApplyToDynamicSequence;
            stream.WriteByte((byte)flags);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 56)
        {
            IncludePendingResume = stream.ReadBoolByte();
            ApplyToStateTransitions = stream.ReadBoolByte();
            ApplyToDynamicSequence = stream.ReadBoolByte();
        }
        else if (version <= 62)
        {
            // TODO: Not sure
        }
        else
        {
            ActiveFlagsInner flags = (ActiveFlagsInner)stream.ReadByte();
            IncludePendingResume = flags.HasFlag(ActiveFlagsInner.IncludePendingResume);
            ApplyToStateTransitions = flags.HasFlag(ActiveFlagsInner.ApplyToStateTransitions);
            ApplyToDynamicSequence = flags.HasFlag(ActiveFlagsInner.ApplyToDynamicSequence);
        }
    }
}
