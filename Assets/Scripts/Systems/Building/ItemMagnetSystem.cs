using Helpers;
using Repository;
using Services.Collision;
using Services.ItemPickup;
using Settings.Building;
using Systems.Core;
using UnityEngine;

namespace Systems.Building
{
    public class ItemMagnetSystem : IUpdateSystem
    {
        private readonly CameraProvider _cameraProvider;
        private readonly IItemPickupService _itemPickupService;
        private readonly IBuildingSettings _buildingSettings;
        private readonly ISurfaceCollisionService _surfaceCollisionService;
        private readonly RaycastHit[] _result = new RaycastHit[1];

        public ItemMagnetSystem(
            CameraProvider cameraProvider, 
            IItemPickupService itemPickupService,
            IBuildingSettings buildingSettings,
            ISurfaceCollisionService surfaceCollisionService
        )
        {
            _cameraProvider = cameraProvider;
            _itemPickupService = itemPickupService;
            _buildingSettings = buildingSettings;
            _surfaceCollisionService = surfaceCollisionService;
        }
        
        public void Update()
        {
            var camera = _cameraProvider.Camera;
            
            if (camera == null)
                return;
            
            var dir = camera.Transform.Value.forward;
            var pickedItem = _itemPickupService.PickedItem;
            
            if (pickedItem == null)
                return;

            Physics.RaycastNonAlloc(camera.Transform.Value.position, dir, _result, _buildingSettings.MagnetDistance,
                _buildingSettings.BuildingLayer);
            
            Debug.DrawLine(camera.Transform.Value.position, dir * 10f, Color.red);

            foreach (var raycastHit in _result)
            {
                if (raycastHit.transform == null)
                {
                    pickedItem.AttachedToSurface.SetValue(false);
                   
                    pickedItem.LocalPosition.SetValue(ItemOffsetHelper.GetOffset(pickedItem));
                    return;
                }
                
                if (!raycastHit.transform.gameObject.TryGetComponent<IBuildingSurface>(out var buildingSurface))
                    continue;
                
                if (!pickedItem.AllowedSurface.Value.HasFlag(buildingSurface.BuildingSurfaceType))
                    continue;
                
                Debug.DrawLine(raycastHit.point, raycastHit.normal * 10f, Color.red);
                
                // Debug.Log($"raycastHit: {LayerMask.LayerToName(raycastHit.transform.gameObject.layer)}");
                 Debug.Log($"raycastHit name: {raycastHit.transform.gameObject}");
                
                var point = pickedItem.Transform.Value.InverseTransformVector(raycastHit.normal);
                var Up = pickedItem.Transform.Value.InverseTransformVector(pickedItem.Transform.Value.up);
                var rotation = Quaternion.FromToRotation(Up, point);
                
                pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);
            
                var position = raycastHit.point;
                position += raycastHit.normal.normalized * pickedItem.Size.Value.y / 2f;
                pickedItem.Position.SetValue(position);

                if (pickedItem.AttachedSurfaceHash.Value != buildingSurface.Hash)
                {
                    Debug.Log($"1111111 AttachedSurfaceHash");
                    pickedItem.AttachedSurfaceHash.SetValue(buildingSurface.Hash);
                    pickedItem.AttachedToSurface.SetValue(true);
                }
                
                _surfaceCollisionService.CheckCollisionByHash(pickedItem.Transform.Value.GetHashCode());
                
            }
        }
    }
}