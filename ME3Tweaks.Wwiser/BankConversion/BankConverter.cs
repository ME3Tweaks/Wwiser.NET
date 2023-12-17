using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser.BankConversion;

public static class BankConverter{
    public static void ConvertBank(WwiseBank bank, BankSerializationContext from, BankSerializationContext to)
    {
        ConvertBankHeader(bank.BKHD, from, to);

        if (bank.HIRC is not null)
        {
            HircConverter.ConvertHircChunk(bank.HIRC, from, to);
        }
    }

    private static void ConvertBankHeader(BankHeaderChunk bkhd, BankSerializationContext from, BankSerializationContext to)
    {
        bkhd.BankGeneratorVersion = to.Version;
        bkhd.FeedbackInBank = to.UseFeedback;

        // TODO: Better way to convert padding?
        if (to.Version > 76)
        {
            bkhd.Padding.Padding = Array.Empty<byte>();
        }
    }
}