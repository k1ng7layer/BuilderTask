﻿using System.Collections.Generic;
using Entity;
using Repository;
using Services.ItemPickup;
using Settings.Building;
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
        private readonly CameraProvider _cameraProvider;
        private readonly IBuildingSettings _buildingSettings;
        private ItemEntity _pointedItem;
        private readonly List<ItemEntity> _entities = new(20);

        public ItemSelectionService(
            ItemProvider itemProvider, 
            PlayerProvider playerProvider,
            IItemPickupService itemPickupService,
            CameraProvider cameraProvider,
            IBuildingSettings buildingSettings
        )
        {
            _itemProvider = itemProvider;
            _playerProvider = playerProvider;
            _itemPickupService = itemPickupService;
            _cameraProvider = cameraProvider;
            _buildingSettings = buildingSettings;
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
            var cam = _cameraProvider.Camera;
            var dir = cam.Transform.Value.forward;
            
            if (items.Count == 0)
                return null;
            
            if (!Physics.Raycast(cam.Transform.Value.position, dir, out var hit, _buildingSettings.MagnetDistance,
                    _buildingSettings.BuildingLayer))
                return null;

            return !_itemProvider.ItemEntities.TryGetValue(hit.transform.GetHashCode(), out var item) ? null : item;
        }
        
        private List<ItemEntity> FindItemsInFov()
        {
            _entities.Clear();

            var player = _playerProvider.Player;
            
            foreach (var item in _itemProvider.GameItems)
            {
                var dirToItem = (item.Position.Value - player.Position.Value);
               
                if (dirToItem.sqrMagnitude >= 7f * 7f)
                    continue;
                
                _entities.Add(item);
                
            }
            
            return _entities;
        }
    }
}