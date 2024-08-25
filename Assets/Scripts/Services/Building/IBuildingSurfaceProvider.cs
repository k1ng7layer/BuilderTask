using Entity;
using UnityEngine;

namespace Services.Building
{
    public interface IBuildingSurfaceProvider
    {
        bool TryGetValidSurface(ItemEntity itemEntity, out SurfaceInfo surface);
    }

    public readonly struct SurfaceInfo
    {
        public readonly int Hash;
        public readonly Vector3 Normal;
        public readonly Vector3 Point;

        public SurfaceInfo(int hash, Vector3 normal, Vector3 point)
        {
            Hash = hash;
            Normal = normal;
            Point = point;
        }
    }
}