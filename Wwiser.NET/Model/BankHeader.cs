using BinarySerialization;

namespace Wwiser.NET.Model
{
    public class BankHeader : Chunk
    {
        public override string Tag => "BKHD";

        [FieldOrder(0)]
        public uint BankGeneratorVersion { get; set; }

        [FieldOrder(1)]
        public uint SoundBankID { get; set; }

        [FieldOrder(2)]
        public uint LanguageID { get; set; }

        [FieldOrder(3)]
        public uint AltValues { get; set; }

        [FieldOrder(4)]
        public uint ProjectID { get; set; }
    }
}
