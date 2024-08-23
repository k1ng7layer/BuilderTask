using System;
using Services.Input;
using Services.ItemPickup;
using Services.ItemSelection;
using Systems.Core;

namespace Systems.Building
{
    public class EnterBuildingModeSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly IItemSelectionService _itemSelectionService;
        private readonly IItemPickupService _itemPickupService;

        public EnterBuildingModeSystem(
            IPlayerInputService playerInputService, 
            IItemSelectionService itemSelectionService,
            IItemPickupService itemPickupService
        )
        {
            _playerInputService = playerInputService;
            _itemSelectionService = itemSelectionService;
            _itemPickupService = itemPickupService;
        }
        
        public void Initialize()
        {
            _playerInputService.UseButtonClicked += EnterBuildingMode;
        }
        
        public void Dispose()
        {
            _playerInputService.UseButtonClicked -= EnterBuildingMode;
        }

        private void EnterBuildingMode()
        {
            if (_itemSelectionService.SelectedItem == null)
                return;

            if (_itemPickupService.PickedItem != null && !_itemPickupService.PickedItem.Blocked.Value)
            {
                _itemPickupService.ReleaseItem();
            }
            else
            {
                _itemPickupService.PickItem(_itemSelectionService.SelectedItem);
            }
        }
    }
}