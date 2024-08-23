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
        
        private void Update()
        {
            Debug.Log($"LAYER: {gameObject.layer}");
        }
    }
}