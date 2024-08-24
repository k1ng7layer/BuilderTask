using System;
using Services.Input;
using Services.ItemPickup;
using Settings.Input;
using Systems.Core;

namespace Systems.Building
{
    public class ItemRotationSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly IItemPickupService _itemPickupService;
        private readonly IInputSettings _inputSettings;

        public ItemRotationSystem(
            IPlayerInputService playerInputService, 
            IItemPickupService itemPickupService,
            IInputSettings inputSettings
        )
        {
            _playerInputService = playerInputService;
            _itemPickupService = itemPickupService;
            _inputSettings = inputSettings;
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
            var up = transform.InverseTransformVector(_itemPickupService.PickedItem
                .Transform.Value.up);
            
            _itemPickupService.PickedItem.Transform.Value.Rotate(up,  45 * dir);
        }
    }
}