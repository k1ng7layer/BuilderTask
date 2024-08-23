using System.Collections.Generic;
using Entity;

namespace Repository
{
    public class ItemProvider
    {
        private Dictionary<int, ItemEntity> _itemsMap = new();
        
        private readonly List<ItemEntity> _gameItems = new();

        public IReadOnlyList<ItemEntity> GameItems => _gameItems;
        public IReadOnlyDictionary<int, ItemEntity> ItemEntities => _itemsMap;

        public void AddItem(int hash, ItemEntity item)
        {
            _gameItems.Add(item);
            _itemsMap.Add(hash, item);
        }
    }
}