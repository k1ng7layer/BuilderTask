using Entity;
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

            var offset = item.Size.Value.z / 2;

            var position = item.Position.Value;
            position.z = offset + 2f;
            position.x = 0;
            position.y = -1;
            item.Position.SetValue(position);

            PickedItem = item;
            PickedItem.Selected.SetValue(false);
        }

        public void ReleaseItem()
        {
            PickedItem.Parent.SetValue(null);
            PickedItem = null;
        }
    }
}