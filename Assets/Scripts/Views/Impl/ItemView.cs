using Entity;
using Helpers;
using Services.Collision;
using UnityEngine;
using Zenject;

namespace Views.Impl
{
    public class ItemView : EntityView, IBuildingSurface
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private Collider _collider;
        [EnumFlag]
        [SerializeField] private BuildingSurfaceType _buildingSurfaceType;
        [EnumFlag]
        [SerializeField] private BuildingSurfaceType _allowedSurface;
        [Inject] private ISurfaceCollisionService _surfaceCollisionService;
        
        private ItemEntity _itemEntity;
        private Color _originColor;
        private Color _transparent;

        public int Hash => transform.GetHashCode();
        public BuildingSurfaceType BuildingSurfaceType => _buildingSurfaceType;
        public BuildingSurfaceType AllowedSurface => _allowedSurface;

        private void Awake()
        {
            _originColor = _renderer.material.color;

            _transparent = _originColor;
            _transparent.a = .3f;
        }

        public override void LinkEntity(GameEntity entity)
        {
            base.LinkEntity(entity);

            _itemEntity = (ItemEntity)entity;
            
            _itemEntity.Size.SetValue(_collider.bounds.size);

            _itemEntity.Picked.ValueChanged += OnPickedChanged;
            _itemEntity.Selected.ValueChanged += OnSelectedChanged;
            _itemEntity.Layer.ValueChanged += OnLayerChanged;
            _itemEntity.Blocked.ValueChanged += OnBlockedChanged;
            _itemEntity.AttachedToSurface.ValueChanged += AttachedToSurfaceChanged;
            
            OnLayerChanged(_itemEntity.Layer.Value);
        }

        protected override void OnEntityDestroy()
        {
            _itemEntity.Picked.ValueChanged -= OnPickedChanged;
            _itemEntity.Layer.ValueChanged -= OnLayerChanged;
            _itemEntity.Selected.ValueChanged -= OnSelectedChanged;
            _itemEntity.Blocked.ValueChanged -= OnBlockedChanged;
            _itemEntity.AttachedToSurface.ValueChanged -= AttachedToSurfaceChanged;
        }

        private void OnBlockedChanged(bool value)
        {
            _renderer.material.color = value ? Color.red : Color.green;
        }

        private void OnPickedChanged(bool value)
        {
            _collider.isTrigger = value;
        }

        private void AttachedToSurfaceChanged(bool value)
        {
            _renderer.material.color = value ? _originColor : _transparent;
        }

        private void OnLayerChanged(int layer)
        {
            gameObject.layer = layer;
        }

        private void OnSelectedChanged(bool value)
        {
            _renderer.material.color = value ? Color.cyan : _originColor;
        }

        private void Update()
        {
            if (_itemEntity == null)
                return;

            _itemEntity.Position.Value = transform.position;
            _itemEntity.Rotation.Value = transform.rotation;
            _itemEntity.LocalRotation.Value = transform.localRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (LayerMasks.BuildingSurface == (LayerMasks.BuildingSurface | (1 << other.gameObject.layer)))
            {
                _surfaceCollisionService.AddCollision(other.transform.GetHashCode(), transform.GetHashCode());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (LayerMasks.BuildingSurface == (LayerMasks.BuildingSurface | (1 << other.gameObject.layer)))
            {
                _surfaceCollisionService.RemoveCollision(other.transform.GetHashCode(), transform.GetHashCode());
            }
        }
        
        
    }
}