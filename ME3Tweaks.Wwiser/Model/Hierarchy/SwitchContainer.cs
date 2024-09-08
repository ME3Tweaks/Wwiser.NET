using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class SwitchContainer : HircItem, IHasNode
{
    [FieldOrder(0)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();

    [FieldOrder(1)] 
    public GroupType GroupType { get; set; } = new();

    [FieldOrder(2)]
    public uint GroupId { get; set; }

    [FieldOrder(3)]
    public uint DefaultSwitchId { get; set; }

    [FieldOrder(4)]
    [SerializeAs(SerializedType.UInt1)]
    public bool IsContinuousValidation { get; set; }

    [FieldOrder(5)] 
    public Children Children { get; set; } = new();

    [FieldOrder(6)] 
    public uint SwitchGroupCount { get; set; }

    [FieldOrder(7)]
    [FieldCount(nameof(SwitchGroupCount))]
    public List<SwitchGroup> SwitchGroups { get; set; } = new();
    
    [FieldOrder(8)] 
    public uint SwitchParamsCount { get; set; }

    [FieldOrder(9)]
    [FieldCount(nameof(SwitchParamsCount))]
    public List<SwitchParams> SwitchParams { get; set; } = new();
}

public class SwitchParams : IAkIdentifiable, IBinarySerializable
{
    [Ignore]
    public uint Id { get; set; }
    
    [Ignore]
    public bool IsFirstOnly { get; set; }
    
    [Ignore]
    public bool ContinuePlayback { get; set; }
    
    [Ignore]
    public OnSwitchMode OnSwitchMode { get; set; }
    
    [Ignore]
    public float FadeInTime { get; set; }
    
    [Ignore]
    public float FadeOutTime { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        
        stream.Write(BitConverter.GetBytes(Id));
        
        if (version <= 89)
        {
            stream.WriteBoolByte(IsFirstOnly);
            stream.WriteBoolByte(ContinuePlayback);
            stream.Write(BitConverter.GetBytes((uint)OnSwitchMode));
        }
        else
        {
            byte bitVector = 0;
            if (IsFirstOnly) bitVector |= 1 << 0;
            if (ContinuePlayback) bitVector |= 1 << 1;
            stream.WriteByte(bitVector);

            stream.WriteByte((byte)OnSwitchMode);
        }
        
        stream.Write(BitConverter.GetBytes(FadeInTime));
        stream.Write(BitConverter.GetBytes(FadeOutTime));
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();
        Id = BitConverter.ToUInt32(span);

        if (version <= 89)
        {
            IsFirstOnly = stream.ReadBoolByte();
            ContinuePlayback = stream.ReadBoolByte();
            
            read = stream.Read(span);
            if (read != 4) throw new Exception();
            OnSwitchMode = (OnSwitchMode)BitConverter.ToUInt32(span);
        }
        else
        {
            var bitVector = stream.ReadByte();
            IsFirstOnly = (bitVector & (1 << 0)) == 1 << 0;
            ContinuePlayback = (bitVector & (1 << 1)) == 1 << 1;

            OnSwitchMode = (OnSwitchMode)((byte)stream.ReadByte() & 0x7);
        }
        
        
        read = stream.Read(span);
        if (read != 4) throw new Exception();
        FadeInTime = BitConverter.ToSingle(span);
        
        read = stream.Read(span);
        if (read != 4) throw new Exception();
        FadeOutTime = BitConverter.ToSingle(span);
    }
}

public class SwitchGroup : AkIdentifiable
{
    [FieldOrder(0)]
    public uint ItemCount { get; set; }

    [FieldOrder(1)] 
    [FieldCount(nameof(ItemCount))]
    public List<uint> ItemIds { get; set; } = new();
}