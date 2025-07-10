using BinarySerialization;
using ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public abstract class HircItem : AkIdentifiable
{
    [Ignore]
    public abstract HircType HircType { get; }
}