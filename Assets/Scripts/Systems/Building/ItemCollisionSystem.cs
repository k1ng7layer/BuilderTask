using Services.Collision;
using Services.ItemPickup;
using Systems.Core;

namespace Systems.Building
{
    public class ItemCollisionSystem : IUpdateSystem
    {
        private readonly ISurfaceCollisionService _surfaceCollisionService;
        private readonly IItemPickupService _itemPickupService;

        public ItemCollisionSystem(
            ISurfaceCollisionService surfaceCollisionService, 
            IItemPickupService itemPickupService
        )
        {
            _surfaceCollisionService = surfaceCollisionService;
            _itemPickupService = itemPickupService;
        }
        
        public void Update()
        {
            var item = _itemPickupService.PickedItem;
            
            if (item == null)
                return;
            
            if (!item.AttachedToSurface.Value)
                return;
            
            var hash = item.Transform.Value.GetHashCode();
            
            var hasCollision = _surfaceCollisionService.CheckCollisionByHash(hash);
                
            item.Blocked.SetValue(hasCollision);
        }
    }
}