using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public class PositioningFlags : IBinarySerializable
{
    [Ignore]
    public int CenterPct { get; set; }
    
    [Ignore]
    public float PanRL { get; set; }
    
    [Ignore]
    public float PanFR { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var parent = serializationContext.FindAncestor<PositioningChunk>();
        if (parent.HasPositioning)
        {
            if (version <= 56)
            {
                stream.Write(BitConverter.GetBytes(CenterPct));
                stream.Write(BitConverter.GetBytes(PanRL));
                stream.Write(BitConverter.GetBytes(PanFR));
            }

            if (version <= 89)
            {
                if (version > 72) stream.WriteBoolByte(parent.Has2DPositioning);

                stream.WriteBoolByte(parent.Has3DPositioning);
                if ((!parent.Has3DPositioning && version <= 72) || parent.Has2DPositioning)
                {
                    stream.WriteBoolByte(parent.HasPanner);
                }
            }
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var parent = serializationContext.FindAncestor<PositioningChunk>();
        var reader = new BinaryReader(stream);

        if (parent.HasPositioning)
        {
            if (version <= 56)
            {
                CenterPct = reader.ReadInt32();
                PanRL = reader.ReadSingle();
                PanRL = reader.ReadSingle();
            }

            if (version <= 89)
            {
                if (version > 72) parent.Has2DPositioning = reader.ReadBoolean();

                parent.Has3DPositioning = reader.ReadBoolean();
                if ((!parent.Has3DPositioning && version <= 72) || parent.Has2DPositioning)
                {
                    parent.HasPanner = reader.ReadBoolean();
                }
            }
        }
    }
}