using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser.BankConversion;

public static class WwiseBankConverter
{
    public static void ConvertBank(WwiseBank bank, uint targetVersion)
    {
        ConvertBank(bank, GetSerializationContext(bank) with { Version = targetVersion });
    }
    
    public static void ConvertBank(WwiseBank bank, BankSerializationContext targetContext)
    {
        if (!CompatibleVersions.IsParseableVersion(targetContext.Version))
        {
            throw new ArgumentException($"Cannot convert bank to version {targetContext.Version}. This version is not supported by Wwiser.NET.", nameof(targetContext));
        }
        
        
        var fromContext = GetSerializationContext(bank);
        ConvertBankHeader(bank.BKHD, fromContext, targetContext);

        if (bank.HIRC is not null)
        {
            HircConverter.ConvertHircChunk(bank.HIRC, fromContext, targetContext);
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
    
    private static BankSerializationContext GetSerializationContext(WwiseBank bank)
    {
        return new BankSerializationContext(Version: bank.BKHD.BankGeneratorVersion, UseModulator: false, UseFeedback: bank.BKHD.FeedbackInBank);
    }
}