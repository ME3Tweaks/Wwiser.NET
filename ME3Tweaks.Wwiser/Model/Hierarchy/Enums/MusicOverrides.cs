namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

[Flags]
public enum MusicOverrides : byte
{
    OverrideParentMidiTempo = 1 << 0,
    OverrideParentMidiTarget = 1 << 1,
    MidiTargetTypeBus = 1 << 2
}