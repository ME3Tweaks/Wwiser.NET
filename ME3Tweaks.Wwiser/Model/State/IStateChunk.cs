using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public interface IStateChunk
{
    public VarCount StateGroupsCount { get; set; }
    public List<StateGroupChunk> GroupChunks { get; set; }
}