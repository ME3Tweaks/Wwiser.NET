using System.Collections.Generic;
using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model.State;

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