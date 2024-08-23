using System.Collections.Generic;
using Entity;
using Repository;
using Services.ItemPickup;
using UnityEngine;
using Zenject;

namespace Services.ItemSelection.Impl
{
    public class ItemSelectionService : IItemSelectionService, 
        ITickable
    {
        private readonly ItemProvider _itemProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly IItemPickupService _itemPickupService;
        private ItemEntity _pointedItem;
        private readonly List<ItemEntity> _entities = new(20);

        public ItemSelectionService(
            ItemProvider itemProvider, 
            PlayerProvider playerProvider,
            IItemPickupService itemPickupService
        )
        {
            _itemProvider = itemProvider;
            _playerProvider = playerProvider;
            _itemPickupService = itemPickupService;
        }

        public ItemEntity SelectedItem { get; private set; }
        
        public void Tick()
        {
            if (_itemPickupService.PickedItem != null)
                return;
            
            var itemsFromFov = FindItemsInFov();
            _pointedItem = FindNearestItem(itemsFromFov);
            
            if (SelectedItem != null)
                SelectedItem.Selected.SetValue(SelectedItem == _pointedItem);

            SelectedItem = _pointedItem; 
        }

        private ItemEntity FindNearestItem(List<ItemEntity> items)
        {
            var player = _playerProvider.Player;

            float nearestDist2 = float.MaxValue;
            ItemEntity nearest = null;

            foreach (var item in items)
            {
                var dist2 = (item.Position.Value - player.Position.Value).sqrMagnitude;

                if (dist2 < nearestDist2)
                {
                    nearestDist2 = dist2;
                    nearest = item;
                }
            }

            return nearest;
        }

        private List<ItemEntity> FindItemsInFov()
        {
            _entities.Clear();

            var player = _playerProvider.Player;
            var cameraForward = player.Rotation.Value * Vector3.forward;
            
            foreach (var item in _itemProvider.GameItems)
            {
                var dirToItem = (item.Position.Value - player.Position.Value).normalized;
                var dot = Vector3.Dot(cameraForward, dirToItem);
                //Debug.Log($"DOT: {dot}, cameraForward: {cameraForward}");
                if (dot < 0.94f)
                    continue;
                
                _entities.Add(item);
            }

            return _entities;
        }
    }
}