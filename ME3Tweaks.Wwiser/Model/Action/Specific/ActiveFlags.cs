namespace ME3Tweaks.Wwiser.Model.Action.Specific;

[Flags]
public enum ActiveFlags : byte
{
    IncludePendingResume = 1 << 0,
    ApplyToStateTransitions = 1 << 1,
    ApplyToDynamicSequence = 1 << 2,
}