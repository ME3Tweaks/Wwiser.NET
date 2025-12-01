using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public class State : HircItem
{
    [FieldOrder(0)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    public StateParametersV56 Parameters { get; set; } = new StateParametersV56();
    
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.GreaterThan)]
    public PropBundleFloat Prop { get; set; } = new PropBundleFloat();
    
    [Ignore] 
    public override HircType HircType => HircType.State;
}

public class StateParametersV56
{
    [FieldOrder(0)]
    public float Volume { get; set; }
    
    [FieldOrder(1)]
    public float LFEVolume { get; set; }
    
    [FieldOrder(2)]
    public float Pitch { get; set; }
    
    [FieldOrder(3)]
    public float LPF { get; set; }
    
    [FieldOrder(4)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public VolumeMeaning VolumeValueMeaning { get; set; }
    
    [FieldOrder(5)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public VolumeMeaning LFEValueMeaning { get; set; }
    
    [FieldOrder(6)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public VolumeMeaning PitchValueMeaning { get; set; }
    
    [FieldOrder(7)]
    [SerializeWhenVersion(52, ComparisonOperator.LessThanOrEqual)]
    public VolumeMeaning LPFValueMeaning { get; set; }
}

public enum VolumeMeaning : byte
{
    Default = 0,
    Independent = 1,
    Offset = 2,
}

public class PropBundleFloat
{
    [FieldOrder(0)] 
    public V126ByteCount PropCount { get; set; } = new();
    
    [FieldOrder(1)]
    [FieldCount($"{nameof(PropCount)}.{nameof(PropCount.Value)}")]
    public List<StateParameterId> PropIds { get; set; } = new();
    
    [FieldOrder(2)]
    [FieldCount($"{nameof(PropCount)}.{nameof(PropCount.Value)}")]
    public List<float> PropValues { get; set; } = new();
    
}
