using BinarySerialization;
using ME3Tweaks.Wwiser.Model.RTPC;

namespace ME3Tweaks.Wwiser.Model;

public class EnvironmentSettingsChunk : Chunk
{
    public override string Tag => @"ENVS";
    
    [FieldOrder(1)]
    [FieldCount(6)]
    public List<EnvironmentsCurve> EnvironmentSettings { get; set;  } = new List<EnvironmentsCurve>();
}

public class EnvironmentSettings
{
    public EnvSettingY CurveObs { get; set; }
    public EnvSettingY CurveOcc { get; set; }
    public EnvSettingY? CurveDiff { get; set; }
    public EnvSettingY? CurveTrans{ get; set; }
}

public class EnvSettingY
{
    public EnvironmentsCurve CurveVol { get; set;  }
    public EnvironmentsCurve CurveLPF { get; set;  }
    public EnvironmentsCurve? CurveHPF { get; set;  }
}

public class EnvironmentsCurve
{
    [FieldOrder(1)]
    [SerializeAs(SerializedType.UInt1)]
    public bool CurveEnabled { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhen(nameof(CurveEnabled), true)] // ??
    public RtpcConversionTable  ConversionTable { get; set; }
}
