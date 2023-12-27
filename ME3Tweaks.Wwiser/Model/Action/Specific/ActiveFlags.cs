using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy;

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
        // TODO: IncludePendingResume on stop?
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var action = serializationContext.FindAncestor<HircItem>() as Hierarchy.Action;
        var notStop = action?.Type.Value != ActionTypeValue.Stop;
        if (version <= 56)
        {
            stream.Write(BitConverter.GetBytes((uint)(IncludePendingResume ? 0 : 1)));
            stream.Write(BitConverter.GetBytes((uint)(ApplyToStateTransitions ? 0 : 1)));
            stream.Write(BitConverter.GetBytes((uint)(ApplyToDynamicSequence ? 0 : 1)));
        }
        else if (version <= 62)
        {
            // TODO: Not sure
        }
        else
        {
            ActiveFlagsInner flags = 0;
            if (IncludePendingResume && notStop) flags |= ActiveFlagsInner.IncludePendingResume;
            if (ApplyToStateTransitions) flags |= ActiveFlagsInner.ApplyToStateTransitions;
            if (ApplyToDynamicSequence) flags |= ActiveFlagsInner.ApplyToDynamicSequence;
            stream.WriteByte((byte)flags);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var reader = new BinaryReader(stream);
        if (version <= 56)
        {
            IncludePendingResume = reader.ReadUInt32() != 1;
            ApplyToStateTransitions = reader.ReadUInt32() != 1;
            ApplyToDynamicSequence = reader.ReadUInt32() != 1;
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
