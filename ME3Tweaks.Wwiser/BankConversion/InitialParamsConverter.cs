using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.BankConversion;

public class InitialParamsConverter(NodeBaseParameters node) : IWwiseConverter
{
    public bool ShouldConvert(BankSerializationContext from, BankSerializationContext to)
    {
        return (from.Version <= 56 && to.Version > 56)
               || (from.Version > 56 && to.Version <= 56);
    }

    public void Convert(BankSerializationContext from, BankSerializationContext to)
    {
        if (from.Version <= 56)
        {
            node.InitialParams62 = ConvertUpVersion(node.InitialParams56);
        }
    }

    private static InitialParamsV62 ConvertUpVersion(InitialParamsV56 from)
    {
        var i = new InitialParamsV62();
        
        
        return i;
    }
}