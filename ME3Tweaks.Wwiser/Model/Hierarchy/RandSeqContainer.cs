using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class RandSeqContainer : HircItem, IHasNode
{
    [FieldOrder(0)]
    public NodeBaseParameters NodeBaseParameters { get; set; } = new();
    
    [FieldOrder(1)]
    public ushort LoopCount { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(72, ComparisonOperator.GreaterThan)]
    public ushort LoopModMin { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(72, ComparisonOperator.GreaterThan)]
    public ushort LoopModMax { get; set; }

    [FieldOrder(4)] 
    public RangedParameterFloat TransitionTime { get; set; } = new();
    
    [FieldOrder(5)]
    public ushort AvoidRepeatCount { get; set; }
    
    [FieldOrder(6)]
    [SerializeWhenVersion(36)]
    public ushort Unknown { get; set; }
    
    //TODO: There's some bullshit on V44 and V45 here - relevant for ME2!
    
    [FieldOrder(7)]
    public TransitionMode TransitionMode { get; set; }
    
    [FieldOrder(8)]
    public RandomMode RandomMode { get; set; }
    
    [FieldOrder(9)]
    public ContainerMode Mode { get; set; }

    [FieldOrder(10)] 
    public RanSeqFlags RanSeqFlags { get; set; } = new();
    
    [FieldOrder(11)] 
    public Children Children { get; set; } = new();

    [FieldOrder(12)] 
    public Playlist Playlist { get; set; } = new();
}

public class RanSeqFlags : IBinarySerializable
{
    [Ignore]
    public bool IsUsingWeight { get; set; }
    
    [Ignore]
    public bool ResetPlayListAtEachPlay { get; set; }
    
    [Ignore]
    public bool IsRestartBackward { get; set; }
    
    [Ignore]
    public bool IsContinuous { get; set; }
    
    [Ignore]
    public bool IsGlobal { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 89)
        {
            stream.WriteBoolByte(IsUsingWeight);
            stream.WriteBoolByte(ResetPlayListAtEachPlay);
            stream.WriteBoolByte(IsRestartBackward);
            stream.WriteBoolByte(IsContinuous);
            stream.WriteBoolByte(IsGlobal);
        }
        else
        {
            RanSeqInner f = 0x0;
            if (IsUsingWeight) f |= RanSeqInner.IsUsingWeight;
            if (ResetPlayListAtEachPlay) f |= RanSeqInner.ResetPlayListAtEachEnd;
            if (IsRestartBackward) f |= RanSeqInner.IsRestartBackward;
            if (IsContinuous) f |= RanSeqInner.IsContinuous;
            if (IsGlobal) f |= RanSeqInner.IsGlobal;
            stream.WriteByte((byte)f);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        if (version <= 89)
        {
            IsUsingWeight = stream.ReadBoolByte();
            ResetPlayListAtEachPlay = stream.ReadBoolByte();
            IsRestartBackward = stream.ReadBoolByte();
            IsContinuous = stream.ReadBoolByte();
            IsGlobal = stream.ReadBoolByte();
        }
        else
        {
            var f = (RanSeqInner)stream.ReadByte();
            IsUsingWeight = f.HasFlag(RanSeqInner.IsUsingWeight);
            ResetPlayListAtEachPlay = f.HasFlag(RanSeqInner.ResetPlayListAtEachEnd);
            IsRestartBackward = f.HasFlag(RanSeqInner.IsRestartBackward);
            IsContinuous = f.HasFlag(RanSeqInner.IsContinuous);
            IsGlobal = f.HasFlag(RanSeqInner.IsGlobal);
        }
    }

    [Flags]
    public enum RanSeqInner : byte
    {
        IsUsingWeight = 1 << 0,
        ResetPlayListAtEachEnd = 1 << 1,
        IsRestartBackward = 1 << 2,
        IsContinuous = 1 << 3,
        IsGlobal = 1 << 4
    }
}

public class Playlist
{
    [FieldOrder(0)]
    public ushort PlaylistLength { get; set; }

    [FieldOrder(1)]
    [FieldCount(nameof(PlaylistLength))]
    public List<PlaylistItem> Items { get; set; } = new();
}

public class PlaylistItem : IBinarySerializable, IAkIdentifiable
{
    [Ignore]
    public uint Id { get; set; }
    
    [Ignore]
    public int Weight { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        stream.Write(BitConverter.GetBytes(Id));
        if (version <= 56)
        {
            stream.WriteByte((byte)Weight);
        }
        else
        {
            stream.Write(BitConverter.GetBytes(Weight));
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        Span<byte> span = stackalloc byte[4];
        var read = stream.Read(span);
        if (read != 4) throw new Exception();
        Id = BitConverter.ToUInt32(span);
        
        if (version <= 56)
        {
            Weight = stream.ReadByte();
        }
        else
        {
            read = stream.Read(span);
            if (read != 4) throw new Exception();
            Weight = BitConverter.ToInt32(span);
        }
    }
}

public enum TransitionMode : byte
{
    Disabled,
    CrossFadeAmp,
    CrossFadePower,
    Delay,
    SampleAccurate,
    TriggerRate
}

public enum RandomMode : byte
{
    Normal,
    Shuffle
}

public enum ContainerMode : byte
{
    Random,
    Sequence
}