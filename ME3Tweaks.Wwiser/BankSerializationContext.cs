namespace ME3Tweaks.Wwiser;

/// <summary>
/// Common contextual data that is is needed for serialization, deserialization, and conversion
/// </summary>
/// <param name="Version">Wwise build version to target</param>
/// <param name="UseModulator">For testing only - whether HIRC object uses modulator</param>
/// /// <param name="UseFeedback">Whether bank uses feedback in bank - unsure what this does</param>
public record BankSerializationContext(uint Version, bool UseModulator = false, bool UseFeedback = false);