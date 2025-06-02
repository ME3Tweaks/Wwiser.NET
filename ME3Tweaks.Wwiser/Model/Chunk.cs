using BinarySerialization;

namespace ME3Tweaks.Wwiser.Model
{
    public abstract class Chunk
    {
        [Ignore]
        public abstract string Tag { get; }

        public virtual bool IsAllowedInVersion(uint version)
        {
            return true;
        }
    }
}
