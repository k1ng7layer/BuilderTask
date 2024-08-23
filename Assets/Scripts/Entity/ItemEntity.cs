using System.Collections.Generic;
using Helpers;
using ReactiveProperty;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : GameEntity
    {
        public Property<bool> Picked { get; private set; } = new();
        public Property<bool> Selected { get; private set; } = new();
        public Property<bool> AttachedToSurface { get; private set; } = new();
        public Property<Vector3> Size { get; private set; } = new();
        public Property<int> Layer { get; private set; } = new();
        public Property<BuildingSurfaceType> AllowedSurface { get; private set; } = new();
        public Property<BuildingSurfaceType> BuildingEntityType { get; private set; } = new();
        public Property<int> AllowedSurfaceMask2 { get; private set; } = new();
        public Property<int> AttachedSurfaceHash { get; private set; } = new();
        public Property<HashSet<int>> Collisions { get; private set; } = new();
    }
}