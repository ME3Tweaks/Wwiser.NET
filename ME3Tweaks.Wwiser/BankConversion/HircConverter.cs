using ME3Tweaks.Wwiser.Model;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.BankConversion;

public static class HircConverter
{
    public static void ConvertHircChunk(HierarchyChunk hirc, BankSerializationContext from,
        BankSerializationContext to)
    {
        var initialParams = new InitialParamsConverter(from, to);
        var baseParams = new NodeBaseParamsStateConverter(from, to);

        foreach (var item in hirc.Items)
        {
            if (NeedsNodeBaseConversion(item))
            {
                if (item.Item is not IHasNode node) break;
                initialParams.Convert(node.NodeBaseParameters);
                baseParams.Convert(node.NodeBaseParameters);
            }
        }
    }

    private static bool NeedsNodeBaseConversion(HircItemContainer i)
    {
        return i.Type.Value is HircType.ActorMixer or HircType.Sound
            or HircType.LayerContainer or HircType.RandomSequenceContainer;
    }
}