namespace ME3Tweaks.Wwiser.BankConversion;

public interface IWwiseConverter
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to);

    public void Convert(BankSerializationContext from, BankSerializationContext to);
}

