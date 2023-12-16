namespace ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

public static class SpatializationHelpers
{
    public static byte GetByteFromMode(SpatializationMode mode, uint version)
    {
        if (version <= 132)
        {
            // HoldListener and HoldEmitter are one flag lower on version 132 and lower
            if (mode.HasFlag(SpatializationMode.HoldEmitterPosAndOrient))
            {
                mode |= SpatializationMode.EnableAttenuation;
                mode &= ~SpatializationMode.HoldEmitterPosAndOrient;
            }

            if (mode.HasFlag(SpatializationMode.HoldListenerOrient))
            {
                mode |= SpatializationMode.HoldEmitterPosAndOrient;
                mode &= ~SpatializationMode.HoldListenerOrient;
            }

            // strip flags only found in higher versions
            mode &= ~SpatializationMode.IsNotLooping;
        }
        
        if (version <= 134) mode &= ~SpatializationMode.EnableDiffraction;
        return (byte)mode;
    }

    public static SpatializationMode GetModeFromByte(byte value, uint version)
    {
        var mode = (SpatializationMode)value;
        if (version <= 132)
        {
            
            // Wipe out any extraneous flags?
            if(version <= 126) mode &= (SpatializationMode)0b0001_1111;
            
            // HoldListener and HoldEmitter are one flag lower on version 132 and lower
            if (mode.HasFlag(SpatializationMode.HoldEmitterPosAndOrient))
            {
                mode |= SpatializationMode.HoldListenerOrient;
                mode &= ~SpatializationMode.HoldEmitterPosAndOrient;
            }

            if (mode.HasFlag(SpatializationMode.EnableAttenuation))
            {
                mode |= SpatializationMode.HoldEmitterPosAndOrient;
                mode &= ~SpatializationMode.EnableAttenuation;
            }

        }

        return mode;
    }

    /// <summary>
    /// Gets a PositioningType based on version and
    /// the HasAutomation and HasDynamic flags
    /// </summary>
    public static PositioningType GetTypeFromBools(bool hasAutomation, bool hasDynamic, 
        uint version)
    {
        if (version <= 72)
        {
            if (hasDynamic) return PositioningType.GameDef3D;
            if (hasAutomation) return PositioningType.UserDef3D;
            return PositioningType.Positioning2D;
        }
        
        if (version <= 89)
        {
            // TODO: Verify this. How do we determine between GameDef and UserDef here?
            if (hasDynamic) return PositioningType.Positioning2D;
            return PositioningType.UserDef3D; // anything that's not Positioning2D?
        }

        return PositioningType.Positioning2D;
    }

    /// <summary>
    /// Gets a SpatializationMode based on version and
    /// the HasAutomation flag
    /// </summary>
    public static SpatializationMode GetModeFromHasAutomation(bool hasAutomation, SpatializationMode modeIn, uint version)
    {
        if (hasAutomation)
        {
            return version switch
            {
                <= 122 => modeIn & ~SpatializationMode.PositionOnly,
                <= 126 => modeIn & ~SpatializationMode.HoldListenerOrient,
                <= 129 => modeIn & ~SpatializationMode.EnableDiffraction,
                _ => modeIn
            };
        }
        else
        {
            return version switch
            {
                <= 122 => (modeIn | SpatializationMode.PositionOnly) & ~SpatializationMode.PositionAndOrientation,
                <= 126 => modeIn | SpatializationMode.HoldListenerOrient,
                <= 129 => modeIn | SpatializationMode.EnableDiffraction,
                _ => modeIn
            };
        }
    }

    /// <summary>
    /// Returns the proper HasAutomation and HasDynamic flags based on the PositioningType
    /// </summary>
    public static (bool, bool) GetBoolFlagsFromType(PositioningType type, bool initialAutomation,
        uint version)
    {
        bool hasAutomation = version switch
        {
            <= 72 => type is PositioningType.UserDef3D,
            <= 89 => type is not PositioningType.Positioning2D,
            _ => initialAutomation
        };

        var hasDynamic = version switch
        {
            <= 72 => type is PositioningType.GameDef3D,
            <= 89 => !hasAutomation,
            _ => false
        };
        return (hasAutomation, hasDynamic);
    }

    /// <summary>
    /// Returns the proper HasAutomation flag based on the SpatializationMode
    /// </summary>
    public static bool GetHasAutomationFromMode(SpatializationMode mode, bool initialAutomation,
        uint version)
    {
        bool hasAutomation = initialAutomation;
        hasAutomation = version switch
        {
            <= 122 => !(mode.HasFlag(SpatializationMode.PositionOnly) && !mode.HasFlag(SpatializationMode.PositionAndOrientation)),
            <= 126 => !mode.HasFlag(SpatializationMode.HoldListenerOrient),
            <= 129 => !mode.HasFlag(SpatializationMode.EnableDiffraction),
            _ => hasAutomation
        };
        return hasAutomation;
    }
}