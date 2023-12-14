using System.Collections;

namespace ME3Tweaks.Wwiser.BankConversion;

public static class BankConverter{
    public static void ConvertBank(WwiseBank bank, BankSerializationContext from, BankSerializationContext to)
    {
        var converters = new List<IWwiseConverter>
        {
            new BankHeaderConverter(bank.BKHD),
            new HircConverter(bank.HIRC)
            
        }.Where(c => c.ShouldConvert(from, to));

        foreach (var c in converters)
        {
            c.Convert(from, to);
        }
    }
}