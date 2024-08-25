using Entity;
using Helpers;
using Services.Building;
using Services.ItemPickup;
using Systems.Core;
using UnityEngine;

namespace Systems.Building
{
    public class ItemMagnetSystem : IUpdateSystem
    {
        private readonly IItemPickupService _itemPickupService;
        private readonly IBuildingSurfaceProvider _buildingSurfaceProvider;

        public ItemMagnetSystem(
            IItemPickupService itemPickupService,
            IBuildingSurfaceProvider buildingSurfaceProvider
        )
        {
            _itemPickupService = itemPickupService;
            _buildingSurfaceProvider = buildingSurfaceProvider;
        }
        
        public void Update()
        {
            var pickedItem = _itemPickupService.PickedItem;
            
            if (pickedItem == null)
                return;

            if (!_buildingSurfaceProvider.TryGetBuildingSurface(out var surface))
            {
                if (pickedItem.AttachedToSurface.Value)
                    ResetPosition(pickedItem);
                
                return;
            }

            if (_buildingSurfaceProvider.ValidateSurface(pickedItem, surface.Type))
            {
                AttachToSurface(pickedItem, surface);
            }
        }

        private void ResetPosition(ItemEntity pickedItem)
        {
            pickedItem.AttachedToSurface.SetValue(false);
            pickedItem.LocalPosition.SetValue(ItemOffsetHelper.GetOffset(pickedItem));
            pickedItem.LocalRotation.SetValue(Quaternion.identity);
            pickedItem.AttachedSurfaceHash.SetValue(-1);
        }

        private void AttachToSurface(ItemEntity pickedItem, SurfaceInfo surface)
        {
            var rotation = CalculateRotationByNormal(pickedItem, surface.Normal);
            
            pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);

            var position = CalculatePositionOnSurface(pickedItem, surface.Point, surface.Normal);
            pickedItem.Position.SetValue(position);
            
            pickedItem.AttachedToSurface.SetValue(true);
            
            if (pickedItem.AttachedSurfaceHash.Value == surface.Hash) return;
            
            pickedItem.AttachedSurfaceHash.SetValue(surface.Hash);
            pickedItem.AttachedToSurface.SetValue(true);
        }

        private Quaternion CalculateRotationByNormal(ItemEntity itemEntity, in Vector3 normal)
        {
            var point = itemEntity.Transform.Value.InverseTransformDirection(normal);
            var worldRotation = itemEntity.Rotation.Value;
            var worldUp = worldRotation * Vector3.up;
            var localUp = itemEntity.Transform.Value.InverseTransformVector(worldUp);
            var rotation = Quaternion.FromToRotation(localUp, point);
            
            return rotation;
        }

        private Vector3 CalculatePositionOnSurface(
            ItemEntity itemEntity, 
            in Vector3 contactPoint, 
            in Vector3 surfaceNormal
        )
        {
            var position = contactPoint + surfaceNormal.normalized * itemEntity.Size.Value.y / 2f;

            return position;
        }
    }
}