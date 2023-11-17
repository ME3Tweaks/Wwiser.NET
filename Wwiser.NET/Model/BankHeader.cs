using BinarySerialization;
using ME3Tweaks.Wwiser.Converters;

namespace ME3Tweaks.Wwiser.Model
{
    public enum LanguageID : uint
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
    
    public class BankHeader : Chunk
    {
        public override string Tag => @"BKHD";

        [FieldOrder(0)]
        public uint BankGeneratorVersion { get; set; }

        [FieldOrder(1)]
        public uint SoundBankId { get; set; }

        [FieldOrder(2)]
        [SerializeWhen(nameof(BankGeneratorVersion), 122, ComparisonOperator.LessThanOrEqual)]
        public LanguageID LanguageId { get; set; }
        
        [FieldOrder(3)]
        [SerializeWhen(nameof(BankGeneratorVersion), 122, ComparisonOperator.GreaterThan)]
        public uint LanguageIdStringHash { get; set; }
        
        [FieldOrder(4)]
        [SerializeWhen(nameof(BankGeneratorVersion), 26, ComparisonOperator.LessThanOrEqual)]
        public UInt64 Timestamp { get; set; }
        
        [FieldOrder(5)]
        [SerializeWhen(nameof(BankGeneratorVersion), true, 
            ConverterType = typeof(BetweenConverter), 
            ConverterParameter = new[] {27, 126})]
        public bool FeedbackInBank { get; set; }

        [FieldOrder(6)]
        [SerializeWhen(nameof(BankGeneratorVersion), 126, ComparisonOperator.GreaterThan)]
        public uint AltValues { get; set; }

        [FieldOrder(7)]
        [SerializeWhen(nameof(BankGeneratorVersion), 76, ComparisonOperator.GreaterThan)]
        public uint ProjectId { get; set; } = 0;
        
        [FieldOrder(8)]
        [SerializeWhen(nameof(BankGeneratorVersion), 141, ComparisonOperator.GreaterThan)]
        public AkBankType SoundBankType { get; set; }
    }
}
