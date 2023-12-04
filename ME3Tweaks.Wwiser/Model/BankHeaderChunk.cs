using BinarySerialization;
using ME3Tweaks.Wwiser.Attributes;

namespace ME3Tweaks.Wwiser.Model
{
    public enum LanguageId : uint
    {
        SFX,
        Arabic,
        Bulgarian,
        Chinese_HK,
        Chinese_PRC,
        Chinese_Taiwan,
        Czech,
        Danish,
        Dutch,
        English_Australia,
        English_India,
        English_UK,
        English_US,
        Finnish,
        French_Canada,
        French_France,
        German,
        Greek,
        Hebrew,
        Hungarian,
        Indonesian,
        Italian,
        Japanese,
        Korean,
        Latin,
        Norwegian,
        Polish,
        Portuguese_Brazil,
        Portuguese_Portugal,
        Romanian,
        Russian,
        Slovenian,
        Spanish_Mexico,
        Spanish_Spain,
        Spanish_US,
        Swedish,
        Turkish,
        Ukrainian,
        Vietnamese
    }

    public enum AkBankType : uint
    {
        User = 0x00,
        Event = 0x1E,
        Bus = 0x1F
    }
    
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
        public uint AltValues { get; set; }
        
        //TODO: Implement bit packed bools in AltValues
        // elif version <= 134:
        //    obj.U32('uAltValues') \
        //    .bit('bUnused', obj.lastval, 0, 0xFFFF) \
        //    .bit('bDeviceAllocated', obj.lastval, 16, 0xFFFF)
        // else:
        //    obj.U32('uAltValues') \
        //    .bit('uAlignment', obj.lastval, 0, 0xFFFF) \
        //    .bit('bDeviceAllocated', obj.lastval, 16, 0xFFFF)

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
        public AkBankType SoundBankType { get; set; }
        
        /// <summary>
        /// Unknown hash of bank?
        /// (&amp;gt; v141)
        /// </summary>
        [FieldOrder(9)]
        [FieldCount(16)]
        [SerializeWhenVersion(141, ComparisonOperator.GreaterThan)]
        public sbyte[]? BankHash { get; set; }
        
        // TODO: Copy any padding over into a byte[]? Will allow proper reserialization.
    }
}
