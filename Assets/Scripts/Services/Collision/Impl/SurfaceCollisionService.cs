using Entity;
using Repository;
using UnityEngine;

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

            var collision =  CheckCollision(item);
            
            item.Blocked.SetValue(collision);

            return collision;
        }

        public void AddCollision(int hash, int itemHash)
        {
            Debug.Log($"1111111 AddCollision");
            
            var item = _itemProvider.ItemEntities[itemHash];

            item.Collisions.Value.Add(hash);
            CheckCollision(item);
        }

        public void RemoveCollision(int hash, int itemHash)
        {
            Debug.Log($"1111111 RemoveCollision");
            
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