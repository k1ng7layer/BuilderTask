using Entity;
using UnityEngine;

namespace Views.Impl
{
    public class PlayerView : EntityView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _itemPlaceholder;
        [SerializeField] private Transform _cameraParent;

        public override void LinkEntity(GameEntity entity)
        {
            base.LinkEntity(entity);

            var playerEntity = (PlayerEntity)entity;
            playerEntity.ItemPlaceholderTransform.SetValue(_cameraParent);
        }

        protected override void OnPositionChanged(Vector3 value)
        {
            _characterController.Move(value);
        }
    }
}