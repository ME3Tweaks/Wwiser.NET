using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class InitialParamsV56 : IBinarySerializable
{
    [Ignore] public float Volume { get; set; }
    [Ignore] public float VolumeMin { get; set; }
    [Ignore] public float VolumeMax { get; set; }
    [Ignore] public float LFE { get; set; }
    [Ignore] public float LFEMin { get; set; }
    [Ignore] public float LFEMax { get; set; }
    [Ignore] public float Pitch { get; set; }
    [Ignore] public float PitchMin { get; set; }
    [Ignore] public float PitchMax { get; set; }
    [Ignore] public float LPF { get; set; }
    [Ignore] public float LPFMin { get; set; }
    [Ignore] public float LPFMax { get; set; }
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        stream.Write(BitConverter.GetBytes(Volume));
        stream.Write(BitConverter.GetBytes(VolumeMin));
        stream.Write(BitConverter.GetBytes(VolumeMax));
        stream.Write(BitConverter.GetBytes(LFE));
        stream.Write(BitConverter.GetBytes(LFEMin));
        stream.Write(BitConverter.GetBytes(LFEMax));
        if (version <= 38)
        {
            stream.Write(BitConverter.GetBytes((int)Pitch));
            stream.Write(BitConverter.GetBytes((int)PitchMin));
            stream.Write(BitConverter.GetBytes((int)PitchMax));
            stream.Write(BitConverter.GetBytes((int)LPF));
            stream.Write(BitConverter.GetBytes((int)LPFMin));
            stream.Write(BitConverter.GetBytes((int)LPFMax));
        }
        else
        {
            stream.Write(BitConverter.GetBytes(Pitch));
            stream.Write(BitConverter.GetBytes(PitchMin));
            stream.Write(BitConverter.GetBytes(PitchMax));
            stream.Write(BitConverter.GetBytes(LPF));
            stream.Write(BitConverter.GetBytes(LPFMin));
            stream.Write(BitConverter.GetBytes(LPFMax));
        }
        
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var reader = new BinaryReader(stream);

        Volume = reader.ReadSingle();
        VolumeMin = reader.ReadSingle();
        VolumeMax = reader.ReadSingle();
        LFE = reader.ReadSingle();
        LFEMin = reader.ReadSingle();
        LFEMax = reader.ReadSingle();
        if (version <= 38)
        {
            Pitch = reader.ReadInt32();
            PitchMin = reader.ReadInt32();
            PitchMax = reader.ReadInt32();
            LPF = reader.ReadInt32();
            LPFMin = reader.ReadInt32();
            LPFMax = reader.ReadInt32();
        }
        else
        {
            Pitch = reader.ReadSingle();
            PitchMin = reader.ReadSingle();
            PitchMax = reader.ReadSingle();
            LPF = reader.ReadSingle();
            LPFMin = reader.ReadSingle();
            LPFMax = reader.ReadSingle();
        }
    }
}