using BinarySerialization;
using ME3Tweaks.Wwiser.Formats;

namespace ME3Tweaks.Wwiser.Model.State;

public class StateGroupChunk : AkIdentifiable
{
    [FieldOrder(0)]
    public StateGroup StateGroup = new StateGroup();
}