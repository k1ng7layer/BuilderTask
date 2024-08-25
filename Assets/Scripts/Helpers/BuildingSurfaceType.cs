using System;

namespace Helpers
{
    [Flags]
    public enum BuildingSurfaceType
    {
        Floor,
        Wall,
        Circle,
        Box,
        None,
    }
}