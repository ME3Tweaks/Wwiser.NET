using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Action;

public class ActionType : IBinarySerializable
{
    [Ignore]
    public ActionTypeValue Value { get; set; }
    
    [Ignore]
    public uint Data { get; set; }

    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        byte output;
        if (version <= 56)
        {
            output = Value switch
            {
                ActionTypeValue.Event1 => 0x20,
                ActionTypeValue.Event2 => 0x30,
                ActionTypeValue.Event3 => 0x40,
                ActionTypeValue.Duck => 0x50, 
                ActionTypeValue.SetSwitch => 0x60,
                ActionTypeValue.SetRTPC => 0x61, 
                ActionTypeValue.BypassFX1 => 0x70, 
                ActionTypeValue.BypassFX2 => 0x80,
                ActionTypeValue.Break => 0x90,
                ActionTypeValue.Trigger => 0xA0,
                ActionTypeValue.Seek => 0xB0,
                >= ActionTypeValue.SetBusVolume1 => (byte)(Value - 2),
                _ => (byte)Value
            };
            var shifted = (uint)(output << 12);
            var value = shifted | Data;
            stream.Write(BitConverter.GetBytes(value));
        }
        else
        {
            output = Value switch
            {
                >= ActionTypeValue.BypassFX1 => (byte)(Value - 3),
                >= ActionTypeValue.SetLPF1 => (byte)(Value - 2),
                _ => (byte)Value
            };
            var value = (ushort)((ushort)(output << 8) | Data);
            stream.Write(BitConverter.GetBytes(value));
        }
        
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        
        //var value = (byte)stream.ReadByte();
        if (version <= 56)
        {
            Span<byte> span = stackalloc byte[4];
            var read = stream.Read(span);
            if (read != 4) throw new Exception();
            uint value = BitConverter.ToUInt32(span);
            
            var enumValue = value >> 12;
            Data = value & 0xFFF;
            
            Value = enumValue switch
            {
                0x20 => ActionTypeValue.Event1,
                0x30 => ActionTypeValue.Event2,
                0x40 => ActionTypeValue.Event3,
                0x50 => ActionTypeValue.Duck,
                0x60 => ActionTypeValue.SetSwitch,
                0x61 => ActionTypeValue.SetRTPC,
                0x70 => ActionTypeValue.BypassFX1,
                0x80 => ActionTypeValue.BypassFX2,
                0x90 => ActionTypeValue.Break,
                0xA0 => ActionTypeValue.Trigger,
                0xB0 => ActionTypeValue.Seek,
                >= 0x0A => (ActionTypeValue)(enumValue + 2),
                _ => (ActionTypeValue)enumValue
            };
        }
        else
        {
            Span<byte> span = stackalloc byte[2];
            var read = stream.Read(span);
            if (read != 2) throw new Exception();
            ushort value = BitConverter.ToUInt16(span);

            var enumValue = value >> 8;
            Data = (uint)value & 0xFF;
            
            Value = enumValue switch
            {
                >= 0x1A => (ActionTypeValue)(enumValue + 3),
                >= 0x0E => (ActionTypeValue)(enumValue + 2),
                _ => (ActionTypeValue)enumValue
            };
        }
    }
}

public enum ActionTypeValue : byte
{
    Unknown,
    Stop,
    Pause,
    Resume,
    Play,
    PlayAndContinue,
    Mute1,
    Mute2,
    SetPitch1,
    SetPitch2,
    SetNone1,
    SetNone2,
    SetBusVolume1,
    SetBusVolume2,
    SetLFE1,
    SetLFE2,
    SetLPF1,
    SetLPF2,
    UseState1,
    UseState2,
    SetState,
    SetGameParameter1,
    SetGameParameter2,
    Event1,
    Event2,
    Event3,
    Duck,
    SetSwitch,
    SetRTPC,
    BypassFX1,
    BypassFX2,
    Break,
    Trigger,
    Seek,
    Release,
    SetHPF1,
    PlayEvent,
    ResetPlaylist,
    PlayEventUnknown,
    SetHPF2,
    SetFX1,
    SetFX2,
    BypassFX3,
    BypassFX4,
    BypassFX5,
    BypassFX6,
    BypassFX7
}