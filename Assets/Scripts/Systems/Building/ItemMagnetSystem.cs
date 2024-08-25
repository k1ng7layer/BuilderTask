using Entity;
using Helpers;
using Services.Building;
using Services.Collision;
using Services.ItemPickup;
using Systems.Core;
using UnityEngine;

namespace Systems.Building
{
    public class ItemMagnetSystem : IUpdateSystem
    {
        private readonly IItemPickupService _itemPickupService;
        private readonly ISurfaceCollisionService _surfaceCollisionService;
        private readonly IBuildingSurfaceProvider _buildingSurfaceProvider;

        public ItemMagnetSystem(IItemPickupService itemPickupService,
            ISurfaceCollisionService surfaceCollisionService,
            IBuildingSurfaceProvider buildingSurfaceProvider
        )
        {
            _itemPickupService = itemPickupService;
            _surfaceCollisionService = surfaceCollisionService;
            _buildingSurfaceProvider = buildingSurfaceProvider;
        }
        
        public void Update()
        {
            var pickedItem = _itemPickupService.PickedItem;
            
            if (pickedItem == null)
                return;

            if (!_buildingSurfaceProvider.TryGetValidSurface(pickedItem, out var surface))
            {
                ResetPosition(pickedItem);
                return;
            }

            AttachToSurface(pickedItem, surface);
                
            _surfaceCollisionService.CheckCollisionByHash(pickedItem.Transform.Value.GetHashCode());
        }

        private void ResetPosition(ItemEntity pickedItem)
        {
            pickedItem.AttachedToSurface.SetValue(false);
            pickedItem.LocalPosition.SetValue(ItemOffsetHelper.GetOffset(pickedItem));
            pickedItem.AttachedSurfaceHash.SetValue(-1);
        }

        private void AttachToSurface(ItemEntity pickedItem, SurfaceInfo surface)
        {
            var point = pickedItem.Transform.Value.InverseTransformVector(surface.Normal);
            var up = pickedItem.Transform.Value.InverseTransformVector(pickedItem.Transform.Value.up);
            var rotation = Quaternion.FromToRotation(up, point);
                
            pickedItem.LocalRotation.SetValue(pickedItem.LocalRotation.Value * rotation);
            
            var position = surface.Point;
            position += surface.Normal.normalized * pickedItem.Size.Value.y / 2f;
            pickedItem.Position.SetValue(position);
            pickedItem.AttachedToSurface.SetValue(true);

            if (pickedItem.AttachedSurfaceHash.Value != surface.Hash)
            {
                pickedItem.AttachedSurfaceHash.SetValue(surface.Hash);
                pickedItem.AttachedToSurface.SetValue(true);
            }
        }
    }
}