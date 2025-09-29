using System.ComponentModel;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model;

internal class EnvironmentSettingsChunk : Chunk
{
    public override string Tag => @"ENVS";
    
    [FieldOrder(1)]
    [FieldCount(nameof(BankSerializationContext.Version), RelativeSourceMode = RelativeSourceMode.SerializationContext, ConverterType = typeof(VersionToEnvsCurveCount))]
    public List<EnvironmentsCurve> EnvironmentSettings { get; set;  } = new();
}

internal class VersionToEnvsCurveCount : IValueConverter
{
    public object Convert(object value, object parameter, BinarySerializationContext context)
    {
        if(value is not uint version)
        {
            throw new ArgumentException("Value is not a valid version number.");
        }

        return version switch // X * Y
        {
            >= 152 => 4 * 3,
            >= 112 => 2 * 3,
            _ => 2 * 2
        };
    }

    public object ConvertBack(object value, object parameter, BinarySerializationContext context)
    {
        throw new NotSupportedException();
    }
}

public class EnvironmentSettings
{
    public EnvSettingY CurveObs { get; set; } = new();
    public EnvSettingY CurveOcc { get; set; } = new();
    public EnvSettingY? CurveDiff { get; set; }
    public EnvSettingY? CurveTrans{ get; set; }
}

public class EnvSettingY
{
    public EnvironmentsCurve CurveVol { get; set; } = new();
    public EnvironmentsCurve CurveLPF { get; set;  } = new();
    public EnvironmentsCurve? CurveHPF { get; set;  }
}

public class EnvironmentsCurve
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool CurveEnabled { get; set; }

    [FieldOrder(2)]
    public RtpcConversionTable ConversionTable { get; set; } = new();
}
