using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.GlobalSettings;

public class STMGParam
{
    [FieldOrder(0)]
    public uint RtpcId { get; set; }
    
    [FieldOrder(1)]
    public float Value { get; set; }
    
    [FieldOrder(2)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public TransitionRampingType RampingType { get; set; }
    
    [FieldOrder(3)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public float RampUp { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public float RampDown { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhenVersion(89, ComparisonOperator.GreaterThan)]
    public BuiltInParam BindToBuildInParam { get; set; }
}

public enum TransitionRampingType : uint
{
    None,
    SlewRate,
    FilteringOverTime
}

public enum BuiltInParam : byte
{
    None,
    StartDistance,
    Azimuth,
    Elevation,
    EmitterCone, //ObjectAngle on <= 125
    Obsruction,
    Occlution,
    ListenerCone,
    Diffraction,
    TransmissionLoss
}