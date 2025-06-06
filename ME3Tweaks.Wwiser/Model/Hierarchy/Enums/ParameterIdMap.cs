namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

using RtpcParameterId = ParameterId.RtpcParameterId;
using ModulatorRtpcParameterId = ParameterId.ModulatorRtpcParameterId;

internal static class ParameterIdMap
{
    internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V128 => new(() =>
        new BiDirectionalMap<byte, RtpcParameterId>
        {
            { 0x2F, RtpcParameterId.UserAuxSendLPF0 },
            { 0x30, RtpcParameterId.UserAuxSendLPF1 },
            { 0x31, RtpcParameterId.UserAuxSendLPF2 },
            { 0x32, RtpcParameterId.UserAuxSendLPF3 },
            { 0x33, RtpcParameterId.UserAuxSendHPF0 },
            { 0x34, RtpcParameterId.UserAuxSendHPF1 },
            { 0x35, RtpcParameterId.UserAuxSendHPF2 },
            { 0x36, RtpcParameterId.UserAuxSendHPF3 },
            { 0x37, RtpcParameterId.GameAuxSendLPF },
            { 0x38, RtpcParameterId.GameAuxSendHPF },
            { 0x3C, RtpcParameterId.UnknownCustom1 },
            { 0x3D, RtpcParameterId.UnknownCustom2 },
            { 0x3E, RtpcParameterId.UnknownCustom3 },
            { 0x40, RtpcParameterId.UnknownCustom4 },
            { 0x41, RtpcParameterId.UnknownCustom5 }
        });
}