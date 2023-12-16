namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

[Flags]
public enum SpatializationMode : byte
{
    None = 0,
    PositionOnly = 1 << 0,
    PositionAndOrientation = 1 << 1,
    Unknown = 1 << 2,
    EnableAttenuation = 1 << 3,
    HoldEmitterPosAndOrient = 1 << 4,
    HoldListenerOrient = 1 << 5,
    EnableDiffraction = 1 << 6,
    IsNotLooping = 1 << 7
}