using System.Collections.Generic;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.Hierarchy;

public interface IStateChunk
{
    public VarCount StateGroupsCount { get; set; }
    public List<StateGroupChunk> GroupChunks { get; set; }
}

public class StateChunk : IStateChunk
{
    [FieldOrder(0)]
    public VarCount StateGroupsCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; }
}

public class StateChunk_Aware : IStateChunk
{
    [FieldOrder(0)]
    public VarCount StatePropsCount { get; set; }
    
    [FieldOrder(1)]
    [FieldCount($"{nameof(StatePropsCount)}.{nameof(StatePropsCount.Value)}")]
    public List<StateProp> PropertyInfo { get; set; }
    
    [FieldOrder(2)]
    public VarCount StateGroupsCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateGroupsCount)}.{nameof(StateGroupsCount.Value)}")]
    public List<StateGroupChunk> GroupChunks { get; set; }
}

public class StateProp
{
    [FieldOrder(0)]
    public VarCount PropertyId { get; set; }
    
    [FieldOrder(1)]
    public AccumType AccumType { get; set; } 
    
    [FieldOrder(2)]
    [SerializeAs(SerializedType.UInt8)]
    [SerializeWhen(nameof(BankSerializationContext.Version), 126,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public bool InDb { get; set; }
}

public class StateGroupChunk : AkIdentifiable
{
    [FieldOrder(1)]
    public SyncType StateSyncType { get; set; }
    
    [FieldOrder(2)]
    public VarCount StateCount { get; set; }
    
    [FieldOrder(3)]
    [FieldCount($"{nameof(StateCount)}.{nameof(StateCount.Value)}")]
    public List<State> States { get; set; }

    public enum SyncType : byte
    {
        Immediate,
        NextGrid,
        NextBar,
        NextBeat,
        NextMarker,
        NextUserMarker,
        EntryMarker,
        ExitMarker,
        ExitNever,
        LastExitPosition
    }
}

public class State : AkIdentifiable
{
    // Lower versions - reference to something else?
    [SerializeWhen(nameof(BankSerializationContext.Version), 145,
        ComparisonOperator.LessThanOrEqual,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public uint StateInstanceId { get; set; }
    
    // Higher versions, data is inlined???? idk
    [SerializeWhen(nameof(BankSerializationContext.Version), 145,
        ComparisonOperator.GreaterThan,
        RelativeSourceMode = RelativeSourceMode.SerializationContext)]
    public PropBundle_Float_UnsignedShort Properties { get; set; }
    
}

public class PropBundle_Float_UnsignedShort
{
    [FieldOrder(0)]
    public ushort PropCount { get; set; }
    
    [FieldOrder(1)]
    [FieldLength(nameof(PropCount))]
    public List<ushort> Ids { get; set; }
    
    [FieldOrder(2)]
    [FieldLength(nameof(PropCount))]
    public List<float> Values { get; set; }
}