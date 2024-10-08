﻿using UnityEngine;

namespace Helpers
{
    public class BuildingSurface : MonoBehaviour, IBuildingSurface
    {
        [EnumFlag]
        [SerializeField] private BuildingSurfaceType _buildingSurfaceType;

        public int Hash => transform.GetHashCode();
        public BuildingSurfaceType BuildingSurfaceType => _buildingSurfaceType;
    }
}