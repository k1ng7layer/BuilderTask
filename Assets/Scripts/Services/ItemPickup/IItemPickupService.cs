using Entity;

namespace Services.ItemPickup
{
    public interface IItemPickupService
    {
        ItemEntity PickedItem { get; }
        void PickItem(ItemEntity item);
        void ReleaseItem();
    }
}