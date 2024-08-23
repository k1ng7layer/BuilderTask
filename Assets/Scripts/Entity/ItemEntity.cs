using Helpers;
using ReactiveProperty;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : GameEntity
    {
        public Property<bool> Picked { get; private set; } = new();
        public Property<bool> Selected { get; private set; } = new();
        public Property<Vector3> Size { get; private set; } = new();

        public Property<LayerMasks.Mask> AllowedSurfaceMask { get; private set; } = new();
        public Property<int> AllowedSurfaceMask2 { get; private set; } = new();
    }
}