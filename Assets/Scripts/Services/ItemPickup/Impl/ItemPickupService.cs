using Entity;
using Helpers;
using Repository;
using UnityEngine;

namespace Services.ItemPickup.Impl
{
    public class ItemPickupService : IItemPickupService
    {
        private readonly CameraProvider _cameraProvider;

        public ItemPickupService(CameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public ItemEntity PickedItem { get; private set; }

        public void PickItem(ItemEntity item)
        {
            item.Parent.SetValue(_cameraProvider.Camera.Transform.Value);
            item.Picked.SetValue(true);
            item.LocalRotation.SetValue(Quaternion.identity);
            item.LocalPosition.SetValue(ItemOffsetHelper.GetOffset(item));

            PickedItem = item;
            PickedItem.Selected.SetValue(false);
            PickedItem.Layer.SetValue(LayerMask.NameToLayer("Default"));
            PickedItem.AttachedToSurface.SetValue(false);
            PickedItem.AttachedSurfaceHash.SetValue(-1);
            PickedItem.Picked.SetValue(true);
        }

        public void ReleaseItem()
        {
            PickedItem.AttachedToSurface.SetValue(true);
            PickedItem.Layer.SetValue(LayerMask.NameToLayer("BuildingSurface"));
            PickedItem.Parent.SetValue(null);
            PickedItem.Picked.SetValue(false);
            PickedItem = null;
        }
    }
}