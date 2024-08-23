using Repository;
using Services.ItemPickup;
using Systems.Core;
using UnityEngine;

namespace Systems.Building
{
    public class ItemMagnetSystem : IUpdateSystem
    {
        private readonly CameraProvider _cameraProvider;
        private readonly IItemPickupService _itemPickupService;
        private readonly RaycastHit[] _result = new RaycastHit[1];

        public ItemMagnetSystem(
            CameraProvider cameraProvider, 
            IItemPickupService itemPickupService
        )
        {
            _cameraProvider = cameraProvider;
            _itemPickupService = itemPickupService;
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

            Physics.RaycastNonAlloc(camera.Position.Value, dir, _result, 10000f,
                pickedItem.AllowedSurfaceMask2.Value);
            
            //Debug.DrawRay(camera.Position.Value, dir, Color.red);

            foreach (var raycastHit in _result)
            {
                if (raycastHit.transform == null)
                    return;
                
                var point = pickedItem.Transform.Value.InverseTransformVector(raycastHit.normal);
                var Up = pickedItem.Transform.Value.InverseTransformVector(pickedItem.Transform.Value.up);
                var rotation = Quaternion.FromToRotation(Up, point);
                
                pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);
            
                var position = raycastHit.point;
                position += raycastHit.normal.normalized * pickedItem.Size.Value.y / 2f;
                pickedItem.Position.SetValue(position);

                for (var index = 0; index < _result.Length; index++)
                {
                    _result[index] = new RaycastHit();
                }
            }
        }
    }
}