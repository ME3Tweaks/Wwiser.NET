using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model
{
    public class BankHeaderChunk : Chunk
    {
        public override string Tag => @"BKHD";
        
        // TODO: Implement version <= 26 of bank header CAkBankMgr__ProcessBankHeader
        
        /// <summary>
        /// Version of Wwise used to generate bank
        /// </summary>
        [FieldOrder(0)]
        public uint BankGeneratorVersion { get; set; }

        /// <summary>
        /// Sound bank ID
        /// </summary>
        [FieldOrder(1)]
        public uint SoundBankId { get; set; }

        /// <summary>
        /// Language ID enum value
        /// (&lt;= v122)
        /// </summary>
        [FieldOrder(2)]
        [SerializeWhenVersion(122, ComparisonOperator.LessThanOrEqual)]
        public LanguageId LanguageId { get; set; }
        
        /// <summary>
        /// String hash of language ID
        /// (&gt; v122)
        /// </summary>
        [FieldOrder(3)]
        [SerializeWhenVersion(122, ComparisonOperator.GreaterThan)]
        public uint LanguageIdStringHash { get; set; }
        
        /// <summary>
        /// Unknown - potentially timestamp?
        /// (&lt;= v26)
        /// </summary>
        [FieldOrder(4)]
        [SerializeWhenVersion(26, ComparisonOperator.LessThanOrEqual)]
        public ulong Timestamp { get; set; }
        
        /// <summary>
        /// Bool of feedback in bank
        /// (v27 &lt;= version &gt;= v126)
        /// </summary>
        [FieldOrder(5)]
        [SerializeAs(SerializedType.UInt4)]
        [SerializeWhenVersionBetween(27, 126)]
        public bool FeedbackInBank { get; set; }

        /// <summary>
        /// Bitpacked bools encoded in AltValues depending on version
        /// </summary>
        [FieldOrder(6)]
        [SerializeWhenVersion(126, ComparisonOperator.GreaterThan)]
        public AltValues AltValues { get; set; }

        /// <summary>
        /// ID of project
        /// (&gt; v76)
        /// </summary>
        [FieldOrder(7)]
        [SerializeWhenVersion(76, ComparisonOperator.GreaterThan)]
        public uint ProjectId { get; set; } = 0;
        
        /// <summary>
        /// Type of bank
        /// (&gt; v141)
        /// </summary>
        [FieldOrder(8)]
        [SerializeWhenVersion(141, ComparisonOperator.GreaterThan)]
        public BankType SoundBankType { get; set; }
        
        /// <summary>
        /// Unknown hash of bank?
        /// (&amp;gt; v141)
        /// </summary>
        [FieldOrder(9)]
        [FieldCount(16)]
        [SerializeWhenVersion(141, ComparisonOperator.GreaterThan)]
        public sbyte[]? BankHash { get; set; }
        
        [FieldOrder(10)]
        public BankHeaderPadding Padding { get; set; } = new();
    }

    public class BankHeaderPadding : IBinarySerializable
    {
        [Ignore]
        public byte[] Padding = Array.Empty<byte>();
        
        public void Serialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
        {
            stream.Write(Padding);
        }

        public void Deserialize(Stream stream, Endianness endianness, BinarySerializationContext serializationContext)
        {
            var version = serializationContext.FindAncestor<BankSerializationContext>().Version;
            var chunkSize = serializationContext.FindAncestor<ChunkContainer>().ChunkSize;
            stream.Seek(GetPaddingSize(version, chunkSize), SeekOrigin.Current);
            Padding = new byte[GetPaddingSize(version, chunkSize)];
        }

        public static uint GetPaddingSize(uint version, uint chunkSize) => version switch
        {
            <= 26 => chunkSize - 0x18,
            <= 76 => chunkSize - 0x10,
            <= 141 => chunkSize - 0x14,
            _ => chunkSize - 0x14 - 0x04 - 0x10
        };
        
        /// <summary>
        /// Sets the necessary padding for the DATA chunk. DATA must start at a multiple of 16 bytes + 8. IE 8, 24, 40, etc
        /// </summary>
        /// <param name="dataChunkOffset">Initial offset of the DATA chunk</param>
        public void SetPadding(long dataChunkOffset)
        {
            var initAlignment = dataChunkOffset % 16;
            Padding = initAlignment switch
            {
                < 8 => new byte[8 - initAlignment],
                > 8 => new byte[8 + (16 - initAlignment)],
                _ => Array.Empty<byte>()
            };
            Array.Fill(Padding, (byte)0);
        }
    }

    [Flags]
    public enum AltValues : uint
    {
        Alignment = 1 << 0,
        DeviceAllocated = 1 << 15
    }
}
