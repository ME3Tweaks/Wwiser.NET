using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

public class PriorityOverrideFlags : IBinarySerializable
{
    public PriorityFlagsInner Value { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 89)
        {
            stream.WriteByte((byte)(Value.HasFlag(PriorityFlagsInner.PriorityOverrideParent) ? 1 : 0));
            stream.WriteByte((byte)(Value.HasFlag(PriorityFlagsInner.PriorityApplyDistFactor) ? 1 : 0));
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
            var overrideParent = stream.ReadByte();
            if (overrideParent is 1) Value &= PriorityFlagsInner.PriorityOverrideParent;
            
            var applyDistFactor = stream.ReadByte();
            if (applyDistFactor is 1) Value &= PriorityFlagsInner.PriorityApplyDistFactor;
        }
        else
        {
            Value = (PriorityFlagsInner)stream.ReadByte();
        }
    }

    [Flags]
    public enum PriorityFlagsInner : byte
    {
        PriorityOverrideParent = 1 << 0,
        PriorityApplyDistFactor = 1 << 1,
        OverrideMidiEventsBehavior = 1 << 2,
        OverrideMidiNoteTracking = 1 << 3,
        EnableMidiNoteTracking = 1 << 4,
        IsMidiBreakLoopOnNoteOff = 1 << 5
    }
}