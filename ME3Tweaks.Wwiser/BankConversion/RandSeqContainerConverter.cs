using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.BankConversion;

public class RandSeqContainerConverter(BankSerializationContext from, BankSerializationContext to)
{
    public void Convert(RandSeqContainer item)
    {
        foreach (var p in item.Playlist.Items)
        {
            p.Weight *= 1000;
        }
    }
}