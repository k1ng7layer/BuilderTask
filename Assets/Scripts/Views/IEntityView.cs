using Entity;
using UnityEngine;

namespace Views
{
    public interface IEntityView
    {
        Transform Transform { get; }
        void LinkEntity(GameEntity entity);
    }
}