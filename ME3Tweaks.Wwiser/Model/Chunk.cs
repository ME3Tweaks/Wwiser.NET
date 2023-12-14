using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model
{
    public abstract class Chunk
    {
        [Ignore]
        public abstract string Tag { get; }

        public static bool IsAllowedInVersion(Chunk chunk, uint version)
        {
            return true;
        }
    }
}
