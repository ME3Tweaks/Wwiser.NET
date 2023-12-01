using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.Plugins;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class BankSourceData
{
    [FieldOrder(0)]
    public Plugin Plugin { get; set; }
    
    [FieldOrder(1)]
    public StreamType StreamType { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 46, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public AudioFormat AudioFormat { get; set; }
    
    [FieldOrder(3)]
    public MediaInformation MediaInformation {get; set;}
    
    [FieldOrder(4)]
    [SerializeWhen(nameof(Plugin), true,
        ConverterType = typeof(HasPluginParamConverter))]
    public PluginParameters PluginParameters { get; set; }
}

public class AudioFormat
{
    [FieldOrder(0)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 26, 
        ComparisonOperator.LessThanOrEqual, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint DataIndex { get; set; }
    
    [FieldOrder(1)]
    public uint SampleRate { get; set; }
    
    [FieldOrder(2)]
    public uint FormatBits { get; set; }
}

public class MediaInformation : IBinarySerializable
{
    [Ignore]
    public uint SourceId { get; set; }
    
    [Ignore]
    public uint FileId { get; set; }
    
    [Ignore]
    public uint FileOffset { get; set; }
    
    [Ignore]
    public uint InMemoryMediaSize { get; set; }
    
    [Ignore]
    public MediaInformationFlags Flags { get; set; }
    
    public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var streamType = serializationContext.FindAncestor<BankSourceData>().StreamType.Value;
        
        stream.Write(BitConverter.GetBytes(SourceId));
        if (version is > 26 and <= 86)
        {
            stream.Write(BitConverter.GetBytes(FileId));
        }

        if (version is > 26 and <= 86 && streamType != StreamType.StreamTypeInner.PrefetchStreaming)
        {
            stream.Write(BitConverter.GetBytes(FileOffset));
        }

        if (version is > 26 and <= 88 && streamType != StreamType.StreamTypeInner.PrefetchStreaming)
        {
            stream.Write(BitConverter.GetBytes(InMemoryMediaSize));
        }
        else if (version > 88)
        {
            stream.Write(BitConverter.GetBytes(InMemoryMediaSize));
        }
        
        if (version > 26)
        {
            var flags = Flags;
            if(version <= 112)
            {
                // On <= 122, HasSource is bit 1. To serialize, set it to Prefetch which is bit 1 on the enum
                if (flags.HasFlag(MediaInformationFlags.HasSource))
                {
                    flags &= MediaInformationFlags.Prefetch;
                    flags &= ~MediaInformationFlags.HasSource;
                }
                // Remove flags not relevant to  this version
                flags &= ~MediaInformationFlags.NonCachable;
            }
            else
            {
                flags &= ~MediaInformationFlags.ExternallySupplied;
            }
            stream.WriteByte((byte)flags);
        }
    }

    public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
    {
        var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
        var streamType = serializationContext.FindAncestor<BankSourceData>().StreamType.Value;
        
        var reader = new BinaryReader(stream);
        SourceId = reader.ReadUInt32();
        if (version is > 26 and <= 86)
        {
            FileId = reader.ReadUInt32();
        }

        if (version is > 26 and <= 86 && streamType != StreamType.StreamTypeInner.PrefetchStreaming)
        {
            FileOffset = reader.ReadUInt32();
        }

        if (version is > 26 and <= 88 && streamType != StreamType.StreamTypeInner.PrefetchStreaming)
        {
            InMemoryMediaSize = reader.ReadUInt32();
        }
        else if (version > 88)
        {
            InMemoryMediaSize = reader.ReadUInt32();
        }

        if (version > 26)
        {
            var flags = (MediaInformationFlags)reader.ReadByte();
            if(version <= 112 && flags.HasFlag(MediaInformationFlags.Prefetch))
            {
                // On <= 122, HasSource is bit 1. To deserialize, replace prefetch with HasSource
                flags &= MediaInformationFlags.HasSource;
                flags &= ~MediaInformationFlags.Prefetch;
            }
            Flags = flags;
        }
        
    }

    [Flags]
    public enum MediaInformationFlags : byte
    {
        IsLanguageSpecific = 0b0000_0001,
        Prefetch = 0b0000_0010,
        ExternallySupplied = 0b0000_0100,
        NonCachable = 0b0000_1000,
        HasSource = 0b1000_0000,
    }
}