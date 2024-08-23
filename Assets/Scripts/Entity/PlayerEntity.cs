using ReactiveProperty;
using UnityEngine;

namespace Entity
{
    public class PlayerEntity : GameEntity
    {
        public Property<Transform> ItemPlaceholderTransform { get; private set; } = new();
    }
}