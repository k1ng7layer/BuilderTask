using ReactiveProperty;
using UnityEngine;

namespace Entity
{
    public class GameEntity
    {
        public Property<Quaternion> Rotation { get; private set; } = new();
        public Property<Quaternion> LocalRotation { get; private set; } = new();
        public Property<Vector3> LocalPosition { get; private set; } = new();
        public Property<Vector3> Position { get; private set; } = new();
        public Property<Transform> Parent { get; private set; } = new();
        public Property<Transform> Transform { get; private set; } = new();
        public Property<bool> Blocked { get; private set; } = new();
    }
}