using Entity;
using Helpers;
using UnityEngine;

namespace Services.Building
{
    public interface IBuildingSurfaceProvider
    {
        bool TryGetBuildingSurface(out SurfaceInfo surface);
        bool ValidateSurface(ItemEntity itemEntity, BuildingSurfaceType surfaceType);
    }

    public readonly struct SurfaceInfo
    {
        public readonly BuildingSurfaceType Type;
        public readonly int Hash;
        public readonly Vector3 Normal;
        public readonly Vector3 Point;

        public SurfaceInfo(
            int hash, 
            Vector3 normal, 
            Vector3 point, 
            BuildingSurfaceType type
        )
        {
            Hash = hash;
            Normal = normal;
            Point = point;
            Type = type;
        }
    }
}