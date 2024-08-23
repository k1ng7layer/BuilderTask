using Repository;
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
        private readonly PlayerProvider _playerProvider;
        private readonly RaycastHit[] _result = new RaycastHit[1];

        public ItemMagnetSystem(
            CameraProvider cameraProvider, 
            IItemPickupService itemPickupService,
            IBuildingSettings buildingSettings,
            PlayerProvider playerProvider
        )
        {
            _cameraProvider = cameraProvider;
            _itemPickupService = itemPickupService;
            _buildingSettings = buildingSettings;
            _playerProvider = playerProvider;
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

            // Physics.RaycastNonAlloc(camera.Position.Value, dir, _result, 10000f,
            //     pickedItem.AllowedSurfaceMask2.Value);
            
            var raytcast = Physics.Raycast(camera.Position.Value, dir, out var hit, _buildingSettings.MagnetDistance,
                pickedItem.AllowedSurfaceMask2.Value);
            
            if (!raytcast)
                return;
            
            Debug.DrawRay(camera.Position.Value, dir, Color.red);

            // foreach (var raycastHit in _result)
            // {
            //     if (raycastHit.transform == null)
            //         return;
            //     
            //     var point = pickedItem.Transform.Value.InverseTransformVector(raycastHit.normal);
            //     var rotation = Quaternion.FromToRotation(Vector3.up, point);
            //     
            //     pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);
            //     
            //     
            //     var position = pickedItem.Position.Value;
            //     position.y = raycastHit.point.y;
            //     pickedItem.Position.SetValue(raycastHit.point);
            //     Debug.Log($"raycastHit.point : {raycastHit.point}");
            //     Debug.Log($"raycastHit.transform: {raycastHit.transform}");
            // }
            
            if (hit.transform == null)
                return;
                
            var point = pickedItem.Transform.Value.InverseTransformVector(hit.normal);
            var rotation = Quaternion.FromToRotation(Vector3.up, point);
                
            pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);


            var position = pickedItem.Position.Value;
            position.y = hit.point.y;
            pickedItem.Position.SetValue(hit.point);
            Debug.Log($"raycastHit.point : {hit.point}");
            // Debug.Log($"raycastHit.transform: {pickedItem.Position.Value}");

            for (var index = 0; index < _result.Length; index++)
            {
                _result[index] = new RaycastHit();
            }
        }
    }
}