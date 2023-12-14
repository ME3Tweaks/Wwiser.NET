using ME3Tweaks.Wwiser.Model;
using ME3Tweaks.Wwiser.Model.Hierarchy;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.BankConversion;

public class HircConverter(HierarchyChunk? hirc) : IWwiseConverter
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to) => hirc is not null;

    public void Convert(BankSerializationContext from, BankSerializationContext to)
    {
        if (hirc is null) return;

        foreach (var c in GetNodeBaseConverters(hirc))
        {
            if (c.ShouldConvert(from, to))
            {
                c.Convert(from, to);
            }
        }
    }

    private static IEnumerable<IWwiseConverter> GetNodeBaseConverters(HierarchyChunk hirc)
    {
        var nodesToConvert = hirc.Items.Where(ItemNeedsNodeBaseConversion)
            .Select(i => i.Item).Cast<IHasNode>().ToList();

        var initialParamsConverters = nodesToConvert.Select(i => new InitialParamsConverter(i.NodeBaseParameters));
        var stateConverters = nodesToConvert.Select(i => new NodeBaseParamsStateConverter(i.NodeBaseParameters));

        return initialParamsConverters.Concat<IWwiseConverter>(stateConverters);
    }

    private static bool ItemNeedsNodeBaseConversion(HircItemContainer i)
    {
        return i.Type.Value is HircType.ActorMixer or HircType.Sound
            or HircType.LayerContainer or HircType.RandomSequenceContainer;
    }
}