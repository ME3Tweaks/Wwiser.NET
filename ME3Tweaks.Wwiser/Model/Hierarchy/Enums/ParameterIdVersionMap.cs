namespace ME3Tweaks.Wwiser.Model.Hierarchy.Enums;

using RtpcParameterId = ParameterId.RtpcParameterId;

/// <summary>
/// Static maps of byte values to RtpcParameterId enum values for versions that have different mapping
/// </summary>
internal static class ParameterIdVersionMap
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
    
    internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V118 => new(() =>
        new BiDirectionalMap<byte, RtpcParameterId>
        {
            { 0x10, RtpcParameterId.Priority },
            { 0x11, RtpcParameterId.MaxNumInstances },
        });

    internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V112 => new(() =>
        new BiDirectionalMap<byte, RtpcParameterId>
        {
            { 0x0, RtpcParameterId.Volume },
            { 0x1, RtpcParameterId.LFE },
            { 0x2, RtpcParameterId.Pitch },
            { 0x3, RtpcParameterId.LPF },
            { 0x4, RtpcParameterId.HPF },
            { 0x5, RtpcParameterId.BusVolume },
            { 0x6, RtpcParameterId.InitialDelay },
            { 0x7, RtpcParameterId.PlayMechanismSpecialTransitionsValue },
            { 0x8, RtpcParameterId.Priority },
            { 0x9, RtpcParameterId.MaxNumInstances },
            { 0xA, RtpcParameterId.PositioningTypeBlend },
            { 0xB, RtpcParameterId.Positioning_Divergence_Center_PCT },
            { 0xC, RtpcParameterId.Positioning_Cone_Attenuation_ON_OFF },
            { 0xD, RtpcParameterId.Positioning_Cone_Attenuation },
            { 0xE, RtpcParameterId.Positioning_Cone_LPF },
            { 0xF, RtpcParameterId.Positioning_Cone_HPF },
            { 0x13, RtpcParameterId.GameAuxSendVolume },
            { 0x14, RtpcParameterId.Position_PAN_X_2D },
            { 0x15, RtpcParameterId.Position_PAN_Y_2D },
            { 0x18, RtpcParameterId.BypassFX0 },
            { 0x19, RtpcParameterId.BypassFX1 },
            { 0x1A, RtpcParameterId.BypassFX2 },
            { 0x1B, RtpcParameterId.BypassFX3 },
            { 0x1C, RtpcParameterId.BypassAllFX },
            { 0x1D, RtpcParameterId.FeedbackVolume },
            { 0x1E, RtpcParameterId.FeedbackLowpass },
            { 0x1F, RtpcParameterId.FeedbackPitch },
            { 0x20, RtpcParameterId.HDRBusThreshold },
            { 0x21, RtpcParameterId.HDRBusReleaseTime },
            { 0x22, RtpcParameterId.HDRBusRatio },
            { 0x23, RtpcParameterId.HDRActiveRange },
            { 0x24, RtpcParameterId.MakeUpGain },
            { 0x25, RtpcParameterId.Position_PAN_X_3D },
            { 0x26, RtpcParameterId.Position_PAN_Y_3D },
            { 0x27, RtpcParameterId.MidiTransposition },
            { 0x28, RtpcParameterId.MidiVelocityOffset },
            { 0x29, RtpcParameterId.PlaybackSpeed },
            { 0x38, RtpcParameterId.UserAuxSendVolume0 },
            { 0x39, RtpcParameterId.UserAuxSendVolume1 },
            { 0x3A, RtpcParameterId.UserAuxSendVolume2 },
            { 0x3B, RtpcParameterId.UserAuxSendVolume3 },
            { 0x3C, RtpcParameterId.OutputBusVolume },
            { 0x3D, RtpcParameterId.OutputBusLPF },
            { 0x3E, RtpcParameterId.OutputBusHPF },
            { 0x3F, RtpcParameterId.MuteRatio }, // 113>=
        });

    internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V88 => new(() =>
        new BiDirectionalMap<byte, RtpcParameterId>
        {
            { 0x0, RtpcParameterId.Volume },
            { 0x1, RtpcParameterId.LFE },
            { 0x2, RtpcParameterId.Pitch },
            { 0x3, RtpcParameterId.LPF },
            { 0x4, RtpcParameterId.BusVolume },
            { 0x5, RtpcParameterId.PlayMechanismSpecialTransitionsValue },
            { 0x6, RtpcParameterId.InitialDelay },
            //{0x7} // Not defined?
            { 0x8, RtpcParameterId.Priority },
            { 0x9, RtpcParameterId.MaxNumInstances },
            { 0xA, RtpcParameterId.PositioningTypeBlend },
            { 0xB, RtpcParameterId.Positioning_Divergence_Center_PCT },
            { 0xC, RtpcParameterId.Positioning_Cone_Attenuation_ON_OFF },
            { 0xD, RtpcParameterId.Positioning_Cone_Attenuation },
            { 0xE, RtpcParameterId.Positioning_Cone_LPF },
            { 0xF, RtpcParameterId.UserAuxSendVolume0 },
            { 0x10, RtpcParameterId.UserAuxSendVolume1 },
            { 0x11, RtpcParameterId.UserAuxSendVolume2 },
            { 0x12, RtpcParameterId.UserAuxSendVolume3 },
            { 0x13, RtpcParameterId.GameAuxSendVolume },
            { 0x14, RtpcParameterId.Position_PAN_X_2D },
            { 0x15, RtpcParameterId.Position_PAN_Y_2D },
            { 0x16, RtpcParameterId.OutputBusVolume },
            { 0x17, RtpcParameterId.OutputBusLPF },
            { 0x18, RtpcParameterId.BypassFX0 },
            { 0x19, RtpcParameterId.BypassFX1 },
            { 0x1A, RtpcParameterId.BypassFX2 },
            { 0x1B, RtpcParameterId.BypassFX3 },
            { 0x1C, RtpcParameterId.BypassAllFX },
            { 0x1D, RtpcParameterId.FeedbackVolume },
            { 0x1E, RtpcParameterId.FeedbackLowpass },
            { 0x1F, RtpcParameterId.FeedbackPitch },
            { 0x20, RtpcParameterId.HDRBusThreshold },
            { 0x21, RtpcParameterId.HDRBusReleaseTime },
            { 0x22, RtpcParameterId.HDRBusRatio },
            { 0x23, RtpcParameterId.HDRActiveRange },
            { 0x24, RtpcParameterId.MakeUpGain },
            { 0x25, RtpcParameterId.Position_PAN_X_3D },
            { 0x26, RtpcParameterId.Position_PAN_Y_3D },
            //{0x40, RtpcParameterId.MaxNumRTPC}
        });
    
        internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V72 => new(() =>
        new BiDirectionalMap<byte, RtpcParameterId>
        {
            { 0x0, RtpcParameterId.Volume },
            { 0x1, RtpcParameterId.LFE },
            { 0x2, RtpcParameterId.Pitch },
            { 0x3, RtpcParameterId.LPF },
            { 0x4, RtpcParameterId.BusVolume },
            { 0x5, RtpcParameterId.PlayMechanismSpecialTransitionsValue },
            //{ 0x6} // Not defined?
            //{0x7} // Not defined?
            { 0x8, RtpcParameterId.Priority },
            { 0x9, RtpcParameterId.MaxNumInstances },
            { 0xA, RtpcParameterId.Positioning_Radius_LPF },
            { 0xB, RtpcParameterId.Positioning_Divergence_Center_PCT },
            { 0xC, RtpcParameterId.Positioning_Cone_Attenuation_ON_OFF },
            { 0xD, RtpcParameterId.Positioning_Cone_Attenuation },
            { 0xE, RtpcParameterId.Positioning_Cone_LPF },
            { 0xF, RtpcParameterId.UserAuxSendVolume0 },
            { 0x10, RtpcParameterId.UserAuxSendVolume1 },
            { 0x11, RtpcParameterId.UserAuxSendVolume2 },
            { 0x12, RtpcParameterId.UserAuxSendVolume3 },
            { 0x13, RtpcParameterId.GameAuxSendVolume },
            { 0x14, RtpcParameterId.Position_PAN_X_2D }, //Position_PAN_RL
            { 0x15, RtpcParameterId.Position_PAN_Y_2D }, // Position_PAN_FR
            { 0x16, RtpcParameterId.OutputBusVolume },
            { 0x17, RtpcParameterId.OutputBusLPF },
            { 0x18, RtpcParameterId.BypassFX0 },
            { 0x19, RtpcParameterId.BypassFX1 },
            { 0x1A, RtpcParameterId.BypassFX2 },
            { 0x1B, RtpcParameterId.BypassFX3 },
            { 0x1C, RtpcParameterId.BypassAllFX },
            { 0x1D, RtpcParameterId.FeedbackVolume },
            { 0x1E, RtpcParameterId.FeedbackLowpass },
            { 0x1F, RtpcParameterId.FeedbackPitch },
            //{0x20, RtpcParameterId.MaxNumRTPC}
        });
        
        internal static Lazy<BiDirectionalMap<byte, RtpcParameterId>> V65 => new(() =>
            new BiDirectionalMap<byte, RtpcParameterId>
            {
                { 0x0, RtpcParameterId.Volume },
                { 0x1, RtpcParameterId.LFE },
                { 0x2, RtpcParameterId.Pitch },
                { 0x3, RtpcParameterId.LPF },
                { 0x4, RtpcParameterId.PlayMechanismSpecialTransitionsValue },
                { 0x5, RtpcParameterId.UnknownCustom1}, // Different for each game / wwise version?
                { 0x6, RtpcParameterId.UnknownCustom2}, // Different for each game / wwise version?
                { 0x7, RtpcParameterId.UnknownCustom3}, // Different for each game / wwise version?
                { 0x8, RtpcParameterId.Priority },
                { 0x9, RtpcParameterId.MaxNumInstances },
                { 0xA, RtpcParameterId.Positioning_Radius_LPF },
                { 0xB, RtpcParameterId.Positioning_Divergence_Center_PCT },
                { 0xC, RtpcParameterId.Positioning_Cone_Attenuation_ON_OFF },
                { 0xD, RtpcParameterId.Positioning_Cone_Attenuation },
                {0xE, RtpcParameterId.Positioning_Cone_LPF },
                { 0xF, RtpcParameterId.UnknownCustom4 }, // Different for each game / wwise version?
                { 0x14, RtpcParameterId.Position_PAN_X_2D }, //Position_PAN_RL
                { 0x15, RtpcParameterId.Position_PAN_Y_2D }, // Position_PAN_FR
                { 0x16, RtpcParameterId.Positioning_Radius_SIM_ON_OFF },
                { 0x17, RtpcParameterId.Positioning_Radius_SIM_Attenuation },
                { 0x18, RtpcParameterId.BypassFX0 },
                { 0x19, RtpcParameterId.BypassFX1 },
                { 0x1A, RtpcParameterId.BypassFX2 },
                { 0x1B, RtpcParameterId.BypassFX3 },
                { 0x1C, RtpcParameterId.BypassAllFX },
                { 0x1D, RtpcParameterId.FeedbackVolume },
                { 0x1E, RtpcParameterId.FeedbackLowpass },
                { 0x1F, RtpcParameterId.FeedbackPitch },
                //{0x20, RtpcParameterId.MaxNumRTPC}
            });
}