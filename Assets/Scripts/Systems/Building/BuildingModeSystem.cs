using System;
using Services.Input;
using Services.ItemPickup;
using Services.ItemSelection;
using Systems.Core;

namespace Systems.Building
{
    public class BuildingModeSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly IItemSelectionService _itemSelectionService;
        private readonly IItemPickupService _itemPickupService;

        public BuildingModeSystem(
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
            var item = _itemSelectionService.SelectedItem;
            
            if (item == null)
                return;

            if (CanRelease())
            {
                _itemPickupService.ReleaseItem();
            }
            else
            {
                if (!item.Picked.Value)
                    _itemPickupService.PickItem(item);
            }
        }

        private bool CanRelease()
        {
            var item = _itemPickupService.PickedItem;
            return item != null && !item.Blocked.Value && item.AttachedToSurface.Value;
        }
    }
}