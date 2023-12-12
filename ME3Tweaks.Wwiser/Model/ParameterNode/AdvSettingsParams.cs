using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class AdvSettingsParams : IBinarySerializable
{
    [Ignore] public VirtualQueueBehavior QueueBehavior { get; set; }
    
    [Ignore] public bool KillNewest { get; set; }
    
    [Ignore] public bool UseVirtualBehavior { get; set; }
    
    [Ignore] public ushort MaxNumInstance { get; set; }
    
    [Ignore] public bool IsGlobalLimit { get; set; }
    
    [Ignore] public BelowThresholdBehavior ThresholdBehavior { get; set; }
    
    [Ignore] public bool Unk1 { get; set; }
    
    [Ignore] public bool IsMaxNumInstOverrideParent { get; set; }
    
    [Ignore] public bool IsVVoicesOptOverrideParent { get; set; }
    
    [Ignore] public bool OverrideHdrEnvelope { get; set; }
    
    [Ignore] public bool OverrideAnalysis { get; set; }
    
    [Ignore] public bool NormalizeLoudness { get; set; }
    
    [Ignore] public bool EnableEnvelope { get; set; }
    
    
    
    public enum VirtualQueueBehavior : byte
    {
        FromBeginning,
        FromElapsedTime,
        Resume
    }
    
    public enum BelowThresholdBehavior : byte
    {
        ContinueToPlay,
        KillVoice,
        SetAsVirtualVoice,
        KillIfOneShotElseVirtual
    }

    [Flags]
    private enum AdvFlags : byte
    {
        KillNewest = 1 << 0,
        UseVirtualBehavior = 1 << 1,
        Unk1 = 1 << 2,
        IgnoreParentMaxNumInst = 1 << 3,
        IsVVoicesOptOverrideParent = 1 << 4
    }
    
    [Flags]
    private enum AdvOverrides : byte
    {
        OverrideHdrEnvelope = 1 << 0,
        OverrideAnalysis = 1 << 1,
        NormalizeLoudness = 1 << 2,
        EnableEnvelope = 1 << 3
    }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version > 89)
        {
            stream.WriteByte((byte)GetFlagsFromProperties());
        }

        if (version <= 36) stream.Write(BitConverter.GetBytes((uint)QueueBehavior));
        else stream.WriteByte((byte)QueueBehavior);
        
        if (version <= 89)
        {
            stream.WriteBoolByte(KillNewest);
            if (version > 53) stream.WriteBoolByte(UseVirtualBehavior);
        }
        stream.Write(BitConverter.GetBytes(MaxNumInstance));
        if (version is <= 89 and > 53) stream.WriteBoolByte(IsGlobalLimit);
        if (version <= 36) stream.Write(BitConverter.GetBytes((uint)ThresholdBehavior));
        else stream.WriteByte((byte)ThresholdBehavior);

        if (version <= 89)
        {
            stream.WriteBoolByte(IsMaxNumInstOverrideParent);
            stream.WriteBoolByte(IsVVoicesOptOverrideParent);
            if (version > 72)
            {
                stream.WriteBoolByte(OverrideHdrEnvelope);
                stream.WriteBoolByte(OverrideAnalysis);
                stream.WriteBoolByte(NormalizeLoudness);
                stream.WriteBoolByte(EnableEnvelope);
            }
        }
        else
        {
            stream.WriteByte((byte)GetOverridesFromProperties());
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var reader = new BinaryReader(stream);
        if (version > 89)
        {
            var flags = (AdvFlags)reader.ReadByte();
            SetPropertiesFromFlags(flags);
        }

        if (version <= 36) QueueBehavior = (VirtualQueueBehavior)(byte)reader.ReadUInt32();
        else QueueBehavior = (VirtualQueueBehavior)reader.ReadByte();
        
        if (version <= 89)
        {
            KillNewest = reader.ReadBoolean();
            if (version > 53) UseVirtualBehavior = reader.ReadBoolean();
        }
        MaxNumInstance = reader.ReadUInt16();
        if (version is <= 89 and > 53) IsGlobalLimit = reader.ReadBoolean();
        if (version <= 36) ThresholdBehavior = (BelowThresholdBehavior)(byte)reader.ReadUInt32();
        else ThresholdBehavior = (BelowThresholdBehavior)reader.ReadByte();

        if (version <= 89)
        {
            IsMaxNumInstOverrideParent = reader.ReadBoolean();
            IsVVoicesOptOverrideParent = reader.ReadBoolean();
            if (version > 72)
            {
                OverrideHdrEnvelope = reader.ReadBoolean();
                OverrideAnalysis = reader.ReadBoolean();
                NormalizeLoudness = reader.ReadBoolean();
                EnableEnvelope = reader.ReadBoolean();
            }
        }
        else
        {
            var overrides = (AdvOverrides)reader.ReadByte();
            SetPropertiesFromOverrides(overrides);
        }
    }

    private AdvFlags GetFlagsFromProperties()
    {
        AdvFlags adv = 0;
        if (KillNewest) adv |= AdvFlags.KillNewest;
        if (UseVirtualBehavior) adv |= AdvFlags.UseVirtualBehavior;
        if (Unk1) adv |= AdvFlags.Unk1;
        if (IsMaxNumInstOverrideParent) adv |= AdvFlags.IgnoreParentMaxNumInst;
        if (IsVVoicesOptOverrideParent) adv |= AdvFlags.IsVVoicesOptOverrideParent;
        return adv;
    }

    private void SetPropertiesFromFlags(AdvFlags adv)
    {
        KillNewest = adv.HasFlag(AdvFlags.KillNewest);
        UseVirtualBehavior = adv.HasFlag(AdvFlags.UseVirtualBehavior);
        Unk1 = adv.HasFlag(AdvFlags.Unk1);
        IsMaxNumInstOverrideParent = adv.HasFlag(AdvFlags.IgnoreParentMaxNumInst); // ??
        IsVVoicesOptOverrideParent = adv.HasFlag(AdvFlags.IsVVoicesOptOverrideParent);
    }

    private AdvOverrides GetOverridesFromProperties()
    {
        AdvOverrides adv = 0;
        if (OverrideHdrEnvelope) adv |= AdvOverrides.OverrideHdrEnvelope;
        if (OverrideAnalysis) adv |= AdvOverrides.OverrideAnalysis;
        if (NormalizeLoudness) adv |= AdvOverrides.NormalizeLoudness;
        if (EnableEnvelope) adv |= AdvOverrides.EnableEnvelope;
        return adv;
    }

    private void SetPropertiesFromOverrides(AdvOverrides adv)
    {
        OverrideHdrEnvelope = adv.HasFlag(AdvOverrides.OverrideHdrEnvelope);
        OverrideAnalysis = adv.HasFlag(AdvOverrides.OverrideAnalysis);
        NormalizeLoudness = adv.HasFlag(AdvOverrides.NormalizeLoudness);
        EnableEnvelope = adv.HasFlag(AdvOverrides.EnableEnvelope);
    }
}