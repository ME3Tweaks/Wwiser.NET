using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.BankConversion;

public class AttenuationConverter(BankSerializationContext from, BankSerializationContext to)
{
    public void Convert(Attenuation item)
    {
        item.Curves.Insert(2, item.Curves[1].Clone());
        item.CurveToUse.CurveMap[2] = 2;
        for(var i = 3; i < item.CurveToUse.CurveMap.Length; i++)
        {
            if (item.CurveToUse.CurveMap[i] > -1) item.CurveToUse.CurveMap[i]++;
        }
        
        foreach (var c in item.Curves)
        {
            RtpcConverter.ConvertRtpcFloatLt0(c);
        }
    }
}