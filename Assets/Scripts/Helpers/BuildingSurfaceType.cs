using System;

namespace Helpers
{
    [Flags]
    public enum BuildingSurfaceType : byte
    {
        None = 0,
        Wall = 1,
        Floor = 2,
        Cube = 3,
        Circle = 4,
    }
}