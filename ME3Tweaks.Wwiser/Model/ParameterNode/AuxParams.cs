using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class AuxParams : IBinarySerializable
{
    // TODO: What do these mean?
    [Ignore]
    public bool Unk1 { get; set; }
    
    [Ignore]
    public bool Unk2 { get; set; }
    
    [Ignore]
    public bool HasAux { get; set; }
    
    [Ignore]
    public bool OverrideGameAuxSends { get; set; }
    
    [Ignore]
    public bool UseGameAuxSends { get; set; }
    
    [Ignore]
    public bool OverrideUserAuxSends { get; set; }
    
    [Ignore]
    public bool OverrideReflectionsAuxBus { get; set; }
    
    [Ignore]
    public uint ReflectionsAuxBus { get; set; }

    [Ignore] 
    public uint[] AuxId { get; set; } = new uint[4];
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;

        if (version <= 89)
        {
            stream.WriteBoolByte(OverrideGameAuxSends);
            stream.WriteBoolByte(UseGameAuxSends);
            stream.WriteBoolByte(OverrideUserAuxSends);
            stream.WriteBoolByte(HasAux);
        }
        else
        {
            stream.WriteByte((byte)GetAuxFlagsFromProperties(version));
        }

        if (HasAux)
        {
            foreach (var id in AuxId.Take(4))
            {
                stream.Write(BitConverter.GetBytes(id));
            }
        }

        if (version > 134)
        {
            stream.Write(BitConverter.GetBytes(ReflectionsAuxBus));
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var reader = new BinaryReader(stream);
        if (version <= 89)
        {
            OverrideGameAuxSends = reader.ReadBoolean();
            UseGameAuxSends = reader.ReadBoolean();
            OverrideUserAuxSends = reader.ReadBoolean();
            HasAux = reader.ReadBoolean();
        }
        else
        {
            var flags = (AuxFlags)reader.ReadByte();
            ApplyPropertiesFromAuxFlags(flags, version);
        }

        if (HasAux)
        {
            AuxId = new uint[4].Select(_ => reader.ReadUInt32()).ToArray();
        }

        if (version > 134)
        {
            ReflectionsAuxBus = reader.ReadUInt32();
        }
    }

    private void ApplyPropertiesFromAuxFlags(AuxFlags f, uint version)
    {
        if (version is 122 or > 135 && f.HasFlag(AuxFlags.OverrideReflections))
        {
            f |= AuxFlags.HasAux;
            f &= ~AuxFlags.OverrideReflections;
        }

        Unk1 = f.HasFlag(AuxFlags.Unk1);
        Unk2 = f.HasFlag(AuxFlags.Unk2);
        HasAux = f.HasFlag(AuxFlags.HasAux);
        OverrideUserAuxSends = f.HasFlag(AuxFlags.OverrideUserAuxSends);
        OverrideReflectionsAuxBus = f.HasFlag(AuxFlags.OverrideReflections);
    }

    private AuxFlags GetAuxFlagsFromProperties(uint version)
    {
        AuxFlags f = 0;

        if (Unk1) f |= AuxFlags.Unk1;
        if (Unk2) f |= AuxFlags.Unk2;
        if (OverrideReflectionsAuxBus) f |= AuxFlags.OverrideReflections;
        if (OverrideUserAuxSends) f |= AuxFlags.OverrideUserAuxSends;
        if (HasAux) f |= (version is 122 or > 135) ? AuxFlags.OverrideReflections : AuxFlags.HasAux;

        return f;
    }

    [Flags]
    public enum AuxFlags : byte
    {
        Unk1 = 1 << 0,
        Unk2 = 1 << 1,
        OverrideUserAuxSends = 1 << 2,
        HasAux = 1 << 3,
        OverrideReflections = 1 << 4
    }
}