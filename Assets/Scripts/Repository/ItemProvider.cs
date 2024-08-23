using System.Collections.Generic;
using Entity;

namespace Repository
{
    public class ItemProvider
    {
        private readonly List<ItemEntity> _gameItems = new();

        public IReadOnlyList<ItemEntity> GameItems => _gameItems;

        public void AddItem(ItemEntity item)
        {
            _gameItems.Add(item);
        }
    }
}