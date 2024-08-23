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

            //var rotation = _itemPickupService.PickedItem.LocalRotation.Value.eulerAngles;
            var rotation = _itemPickupService.PickedItem.Transform.Value.localEulerAngles;
            
            rotation.y += dir * _inputSettings.ItemRotationSensitivity;
            //
             //_itemPickupService.PickedItem.LocalRotation.SetValue(Quaternion.Euler(rotation));

            //_itemPickupService.PickedItem.Transform.Value.localEulerAngles = rotation;
            
            _itemPickupService.PickedItem.Transform.Value.Rotate(_itemPickupService.PickedItem.Transform.Value.InverseTransformVector(_itemPickupService.PickedItem.Transform.Value.up), 1f);
        }
    }
}