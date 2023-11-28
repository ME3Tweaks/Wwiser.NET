namespace ME3Tweaks.Wwiser;

/// <summary>
/// Common contextual data that is made available to every part of the tree as it gets serialized.
/// </summary>
/// <param name="Version">Wwise build version to target</param>
public record BankSerializationContext(uint Version, bool UseModulator = false);