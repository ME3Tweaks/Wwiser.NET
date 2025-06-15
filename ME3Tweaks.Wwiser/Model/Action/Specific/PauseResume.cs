using BinarySerialization;
using ME3Tweaks.Wwiser.SerializationHelpers;

namespace ME3Tweaks.Wwiser.Model.Action.Specific;

public class PauseResume : ISpecificParams
{
    [FieldOrder(1)]
    [SerializeWhenVersion(56, ComparisonOperator.LessThanOrEqual)]
    [SerializeAs(SerializedType.UInt4)]
    public bool IsMaster { get; set; } // TODO: This is a byte on V62 - not relevant to mass effect

    [FieldOrder(2)] 
    public ActiveFlags Flags { get; set; } = new();
}