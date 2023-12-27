using ME3Tweaks.Wwiser.Model.ParameterNode;
using ME3Tweaks.Wwiser.Model.State;

namespace ME3Tweaks.Wwiser.BankConversion;

public class NodeBaseParamsStateConverter(BankSerializationContext from, BankSerializationContext to)
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to)
    {
        return (from.Version <= 52 && to.Version > 52)
               || (from.Version > 52 && to.Version <= 52);
    }

    public void Convert(NodeBaseParameters node)
    {
        if (from.Version <= 52 && to.Version > 52)
        {
            // Convert up versions
            node.StateChunk = new StateChunk
            {
                GroupChunks = new List<StateGroupChunk>
                {
                    new() { StateGroup = node.StateGroup }
                }
            };
        }
        else if (from.Version > 52 && to.Version <= 52)
        {
            // Convert down versions
            node.StateGroup = node.StateChunk.GroupChunks.FirstOrDefault()?.StateGroup
                              ?? new StateGroup();
        }
    }
}