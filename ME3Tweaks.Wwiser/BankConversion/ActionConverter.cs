using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Action;
using ME3Tweaks.Wwiser.Model.ParameterNode;
using Action = ME3Tweaks.Wwiser.Model.Hierarchy.Action;

namespace ME3Tweaks.Wwiser.BankConversion;

public class ActionConverter(BankSerializationContext from, BankSerializationContext to)
{
    public void Convert(Action item)
    {
        if (from.Version <= 56 && to.Version > 56)
        {
            ConvertUp(item);
        }
        else if (from.Version > 56 && to.Version <= 56)
        {
            ConvertDown(item);
        }
    }

    private static void ConvertUp(Action item)
    {
        AddRangedParameter(item.Delay, PropId.DelayTime);

        if (item.ActionParams is Active active)
        {
            AddRangedParameter(active.TransitionTime, PropId.TransitionTime);
        }
        
        return;

        void AddRangedParameter(RangedParameterInt parameter, PropId id)
        {
            if (parameter.Base != 0)
            {
                var paramValue = new InitialParamsV62.ParameterValue(new Uni(parameter.Base));
                item.PropBundle.AddParameter(id, paramValue);
            }
            if (parameter.Min != 0 || parameter.Max != 0)
            {
                item.PropBundle.AddRange(id, new Uni(parameter.Min), new Uni(parameter.Max));   
            }
        }
    }

    private static void ConvertDown(Action item)
    {
        throw new NotImplementedException();
    }
}