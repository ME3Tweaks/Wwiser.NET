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

    public void Convert(RtpcParameterNodeBase node)
    {
        if (from.Version <= 89)
        {
            // Convert up versions
            foreach (var rtpc in node.Rtpcs)
            {
                rtpc.RtpcType = new RtpcType(RtpcType.RtpcTypeInner.GameParameter);
                rtpc.RtpcAccum = new AccumType(AccumType.AccumTypeInner.Additive);

                ConvertRtpcFloatLt0(rtpc.RtpcConversionTable);
            }
        }
    }

    public static void ConvertRtpcFloatLt0(RtpcConversionTable rct)
    {
        foreach (var g in rct.Graph)
        {
            if (g.To < 0)
            {
                // Something close to this - not exactly
                g.To /= 100;
                //g.To = -0.999984681606293f;
            }
        }
    }
}