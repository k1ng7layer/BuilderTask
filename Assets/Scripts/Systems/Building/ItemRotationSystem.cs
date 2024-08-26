using System;
using Services.Input;
using Services.ItemPickup;
using Settings.Building;
using Systems.Core;
using UnityEngine;

namespace Systems.Building
{
    public class ItemRotationSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly IItemPickupService _itemPickupService;
        private readonly IBuildingSettings _buildingSettings;

        public ItemRotationSystem(
            IPlayerInputService playerInputService, 
            IItemPickupService itemPickupService,
            IBuildingSettings buildingSettings
        )
        {
            _playerInputService = playerInputService;
            _itemPickupService = itemPickupService;
            _buildingSettings = buildingSettings;
        }
        
        public void Initialize()
        {
            _playerInputService.ItemRotation += RotateItem;
        }
        
        public void Dispose()
        { 
            _playerInputService.ItemRotation -= RotateItem;
        }

        private void RotateItem(float dir)
        {
            var item = _itemPickupService.PickedItem;
            
            if (item == null)
                return;
            
            var angle = item.OffsetAngle.Value;
            
            angle += _buildingSettings.RotationDeltaDeg * dir;
            angle = Mathf.Repeat(angle, 360f);
            
            item.OffsetAngle.SetValue(angle);
        }
    }
}