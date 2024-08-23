using Entity;
using UnityEngine;

namespace Views.Impl
{
    public class ItemView : EntityView
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private Collider _collider;
        
        private ItemEntity _itemEntity;
        private Color _originColor;

        private void Awake()
        {
            _originColor = _renderer.material.color;
        }

        public override void LinkEntity(GameEntity entity)
        {
            base.LinkEntity(entity);

            _itemEntity = (ItemEntity)entity;
            
            _itemEntity.Selected.ValueChanged += OnSelectedChanged;
            _itemEntity.Size.SetValue(_collider.bounds.size);
        }

        protected override void OnEntityDestroy()
        {
            _itemEntity.Selected.ValueChanged -= OnSelectedChanged;
        }

        private void OnSelectedChanged(bool value)
        {
            Debug.Log($"OnSelectedChanged: {value}");
            _renderer.material.color = value ? Color.cyan : _originColor;
        }

        // protected override void OnPositionChanged(Vector3 value)
        // {
        //     transform.localPosition = value;
        // }

        private void Update()
        {
            if (_itemEntity == null)
                return;

            _itemEntity.Position.Value = transform.position;
             _itemEntity.Rotation.Value = transform.rotation;
            _itemEntity.LocalRotation.Value = transform.localRotation;
            
            Debug.Log($"Local Rotation: {transform.localRotation.eulerAngles}");
            Debug.Log($"Rotation: {transform.rotation.eulerAngles}");
            
            //Debug.Log($"rotation 11111 OLD: {rotation}");
        }
    }
}