using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode;

public class FeedbackInfo : IBinarySerializable
{
    [Ignore]
    public uint Size { get; set; }

    [Ignore] 
    public uint[] Unks { get; set; } = Array.Empty<uint>();
    
    [Ignore]
    public uint BusId { get; set; }
    
    [Ignore]
    public float FeedbackVolume { get; set; }
    
    [Ignore]
    public float FeedbackModifierMin { get; set; }
    
    [Ignore]
    public float FeedbackModifierMax { get; set; }
    
    [Ignore]
    public float FeedbackLPF { get; set; }
    
    [Ignore]
    public float FeedbackLPFModifierMin { get; set; }
    
    [Ignore]
    public float FeedbackLPFModifierMax { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        if (!context.UseFeedback) return;
        if (context.Version <= 26)
        {
            Size = (uint)Unks.Length * sizeof(uint);
            stream.Write(BitConverter.GetBytes(Size));
            foreach (var u in Unks)
            {
                stream.Write(BitConverter.GetBytes(u));
            }
            stream.Write(BitConverter.GetBytes(FeedbackVolume));
        }
        else
        {
            stream.Write(BitConverter.GetBytes(BusId));
            if (BusId != 0)
            {
                stream.Write(BitConverter.GetBytes(FeedbackVolume));
                stream.Write(BitConverter.GetBytes(FeedbackModifierMin));
                stream.Write(BitConverter.GetBytes(FeedbackModifierMax));
                stream.Write(BitConverter.GetBytes(FeedbackLPF));
                stream.Write(BitConverter.GetBytes(FeedbackLPFModifierMin));
                stream.Write(BitConverter.GetBytes(FeedbackLPFModifierMax));
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var context = serializationContext.FindAncestor<BankSerializationContext>();
        var reader = new BinaryReader(stream);
        if (!context.UseFeedback) return;
        if (context.Version <= 26)
        {
            Size = reader.ReadUInt32();
            Unks = new uint[Size / 4];
            Unks = Unks.Select(_ => reader.ReadUInt32()).ToArray();
            FeedbackVolume = reader.ReadSingle();
        }
        else
        {
            BusId = reader.ReadUInt32();
            if (BusId != 0)
            {
                FeedbackVolume = reader.ReadSingle();
                FeedbackModifierMin = reader.ReadSingle();
                FeedbackModifierMax = reader.ReadSingle();
                FeedbackLPF = reader.ReadSingle();
                FeedbackLPFModifierMin = reader.ReadSingle();
                FeedbackLPFModifierMax = reader.ReadSingle();
            }
        }
    }
}