using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.BankConversion;

public class RtpcConverter(BankSerializationContext from, BankSerializationContext to)
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to)
    {
        return (from.Version <= 89 && to.Version > 89)
               || (from.Version > 89 && to.Version <= 89);
    }

    public void Convert(NodeBaseParameters node)
    {
        if (from.Version <= 89)
        {
            // Convert up versions
            foreach (var rtpc in node.Rtpc.Rtpcs)
            {
                rtpc.RtpcType = new RtpcType(RtpcType.RtpcTypeInner.GameParameter);
                rtpc.RtpcAccum = new AccumType(AccumType.AccumTypeInner.Additive);
            }
        }
    }
}