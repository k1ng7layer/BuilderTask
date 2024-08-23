namespace Helpers
{
    public interface IBuildingSurface
    {
        int Hash { get; }
        public BuildingSurfaceType BuildingSurfaceType { get; }
    }
}