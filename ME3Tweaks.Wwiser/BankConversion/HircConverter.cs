using ME3Tweaks.Wwiser.Model;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.BankConversion;

public static class HircConverter
{
    public static void ConvertHircChunk(HierarchyChunk hirc, BankSerializationContext from,
        BankSerializationContext to)
    {
        var initialParams = new InitialParamsConverter(from, to);
        var baseParams = new NodeBaseParamsStateConverter(from, to);
        var rtpcConverter = new RtpcConverter(from, to);
        var attenuationConverter = new AttenuationConverter(from, to);
        var randSeqContainerConverter = new RandSeqContainerConverter(from, to);

        foreach (var item in hirc.Items)
        {
            if (NeedsNodeBaseConversion(item))
            {
                if (item.Item is not IHasNode node) break;
                initialParams.Convert(node.NodeBaseParameters);
                baseParams.Convert(node.NodeBaseParameters);
                rtpcConverter.Convert(node.NodeBaseParameters);
            }

            if (item.Type.Value is HircType.Attenuation)
            {
                if (item.Item is not Attenuation attn) break;
                attenuationConverter.Convert(attn);
            }
            
            if (item.Type.Value is HircType.RandomSequenceContainer)
            {
                if (item.Item is not RandSeqContainer rsc) break;
                randSeqContainerConverter.Convert(rsc);
            }
        }
    }

    private static bool NeedsNodeBaseConversion(HircItemContainer i)
    {
        return i.Type.Value is HircType.ActorMixer or HircType.Sound
            or HircType.LayerContainer or HircType.RandomSequenceContainer;
    }
}