using Entity;
using Repository;

namespace Services.Collision.Impl
{
    public class SurfaceCollisionService : ISurfaceCollisionService
    {
        private readonly ItemProvider _itemProvider;

        public SurfaceCollisionService(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }
        
        public bool CheckCollisionByHash(int itemHash)
        {
            var item = _itemProvider.ItemEntities[itemHash];

            var collision = CheckCollision(item);

            return collision;
        }

        public void AddCollision(int hash, int itemHash)
        {
            var item = _itemProvider.ItemEntities[itemHash];

            item.Collisions.Value.Add(hash);
            CheckCollision(item);
        }

        public void RemoveCollision(int hash, int itemHash)
        {
            var item = _itemProvider.ItemEntities[itemHash];

            item.Collisions.Value.Remove(hash);
            CheckCollision(item);
        }

        private bool CheckCollision(ItemEntity itemEntity)
        {
            foreach (var collision in itemEntity.Collisions.Value)
            {
                if (collision != itemEntity.AttachedSurfaceHash.Value)
                {
                    return true;
                }
            }

            return false;
        }
    }
}