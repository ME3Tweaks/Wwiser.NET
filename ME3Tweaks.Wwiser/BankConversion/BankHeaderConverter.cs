using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser.BankConversion;

public class BankHeaderConverter(BankHeaderChunk bkhd) : IWwiseConverter
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to) => true;

    public void Convert(BankSerializationContext from, BankSerializationContext to)
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