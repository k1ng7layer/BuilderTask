using Entity;
using UnityEngine;

namespace Views.Impl
{
    public class EntityView : MonoBehaviour, 
        IEntityView
    {
        private GameEntity _gameEntity;

        public Transform Transform => transform;

        public virtual void LinkEntity(GameEntity entity)
        {
            _gameEntity = entity;
            
            entity.Position.ValueChanged += OnPositionChanged;
            entity.LocalPosition.ValueChanged += OnLocalPositionChanged;
            entity.Rotation.ValueChanged += OnRotationChanged;
            entity.LocalRotation.ValueChanged += OnLocalRotationChanged;
            entity.Parent.ValueChanged += OnParentChanged;
        }
        
        protected virtual void OnDestroy()
        {
            _gameEntity.Position.ValueChanged -= OnPositionChanged;
            _gameEntity.Rotation.ValueChanged -= OnRotationChanged;
            _gameEntity.LocalPosition.ValueChanged -= OnLocalPositionChanged;
            _gameEntity.LocalRotation.ValueChanged -= OnLocalRotationChanged;
            _gameEntity.Parent.ValueChanged -= OnParentChanged;
            
            OnEntityDestroy();
        }

        protected virtual void OnPositionChanged(Vector3 value)
        {
            transform.position = value;
        }
        
        protected virtual void OnLocalPositionChanged(Vector3 value)
        {
            transform.localPosition = value;
        }
        
        protected virtual void OnRotationChanged(Quaternion value)
        {
            transform.rotation = value;
        }
        
        protected virtual void OnLocalRotationChanged(Quaternion value)
        {
            transform.localRotation = value;
        }

        private void OnParentChanged(Transform value)
        {
            transform.SetParent(value);
        }

        protected virtual void OnEntityDestroy()
        {}
    }
}