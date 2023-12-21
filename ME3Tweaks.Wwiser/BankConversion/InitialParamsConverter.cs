using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.ParameterNode;

namespace ME3Tweaks.Wwiser.BankConversion;

public class InitialParamsConverter(BankSerializationContext from, BankSerializationContext to)
{
    public bool ShouldConvert()
    {
        return (from.Version <= 56 && to.Version > 56)
               || (from.Version > 56 && to.Version <= 56);
    }

    public void Convert(NodeBaseParameters node)
    {
        if (from.Version <= 56)
        {
            node.InitialParams62 = ConvertUpVersion(node.InitialParams56);
        }
        else
        {
            node.InitialParams56 = ConvertDownVersion(node.InitialParams62);
            
            // Potentially mass effect only - set these values that don't exist on higher versions
            node.Priority = 50;
            node.DistOffset = -10;
        }
    }

    private static InitialParamsV56 ConvertDownVersion(InitialParamsV62 from)
    {
        var to = new InitialParamsV56();
        
        for(var i = 0; i < from.ParameterIds.Count; i++)
        {
            switch (from.ParameterIds[i].PropValue)
            {
                case PropId.Volume:
                    to.Volume = from.ParameterValues[i].Value;
                    break;
                case PropId.LFE:
                    to.LFE = from.ParameterValues[i].Value;
                    break;
                case PropId.Pitch:
                    to.Pitch = from.ParameterValues[i].Value; 
                    break;
                case PropId.LPF:
                    to.LPF = from.ParameterValues[i].Value;
                    break;
            }
        }
        
        for(var i = 0; i < from.RangeIds.Count; i++)
        {
            switch (from.RangeIds[i].PropValue)
            {
                case PropId.Volume:
                    to.VolumeMin = from.RangeValues[i].Low.Value;
                    to.VolumeMax = from.RangeValues[i].High.Value;
                    break;
                case PropId.LFE:
                    to.LFEMin = from.RangeValues[i].Low.Value;
                    to.LFEMax = from.RangeValues[i].High.Value;
                    break;
                case PropId.Pitch:
                    to.PitchMin = from.RangeValues[i].Low.Value;
                    to.PitchMax = from.RangeValues[i].High.Value;
                    break;
                case PropId.LPF:
                    to.LPFMin = from.RangeValues[i].Low.Value;
                    to.LPFMax = from.RangeValues[i].High.Value;
                    break;
            }
        }
        return to;
    }

    private static InitialParamsV62 ConvertUpVersion(InitialParamsV56 from)
    {
        var i = new InitialParamsV62();

        AddParameter(PropId.Volume, from.Volume);
        AddRange(PropId.Volume, from.VolumeMin, from.VolumeMax);
        
        AddParameter(PropId.LFE, from.LFE);
        AddRange(PropId.LFE, from.LFEMin, from.LFEMax);
        
        AddParameter(PropId.Pitch, from.Pitch);
        AddRange(PropId.Pitch, from.PitchMin, from.PitchMax);
        
        AddParameter(PropId.LPF, from.LPF);
        AddRange(PropId.LPF, from.LPFMin, from.LPFMax);

        return i;

        void AddParameter(PropId id, float value)
        {
            if (value != 0f)
            {
                i.AddParameter(id, new InitialParamsV62.ParameterValue(new Uni(value)));
            }
        }

        void AddRange(PropId id, float low, float high)
        {
            if (low != 0f || high != 0f)
            {
                i.AddRange(id, new Uni(low), new Uni(high));
            }
        }
    }
}