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
            if (_itemPickupService.PickedItem == null)
                return;

            var transform = _itemPickupService.PickedItem.Transform.Value;
            var worldRotation = _itemPickupService.PickedItem.Rotation.Value;
            var worldUp = worldRotation * Vector3.up;
            var localUp = transform.InverseTransformVector(worldUp);
            var localRotation = _itemPickupService.PickedItem.LocalRotation.Value;
            var delta = Quaternion.AngleAxis(_buildingSettings.RotationDeltaDeg * dir, localUp);
            var rotation = localRotation * delta;
            
            _itemPickupService.PickedItem.LocalRotation.SetValue(rotation);
        }
    }
}