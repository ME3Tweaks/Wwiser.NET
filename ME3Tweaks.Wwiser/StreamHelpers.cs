namespace ME3Tweaks.Wwiser;

public static class StreamHelpers
{
    public static bool ReadBoolByte(this Stream stream)
    {
        var read = stream.ReadByte();
        return read != 0x00;
    }
    
    public static void WriteBoolByte(this Stream stream, bool value)
    {
        stream.WriteByte((byte)(value ? 0x01 : 0x00));
    }
}