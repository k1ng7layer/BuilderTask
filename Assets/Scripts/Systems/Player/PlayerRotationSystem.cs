using Repository;
using Services.Input;
using Services.TimeProvider;
using Systems.Core;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerRotationSystem : IUpdateSystem
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly ITimeProvider _timeProvider;
        private readonly PlayerProvider _playerProvider;

        public PlayerRotationSystem(
            IPlayerInputService playerInputService, 
            ITimeProvider timeProvider, 
            PlayerProvider playerProvider
        )
        {
            _playerInputService = playerInputService;
            _timeProvider = timeProvider;
            _playerProvider = playerProvider;
        }
        
        public void Update()
        {
            var player = _playerProvider.Player;

            if (player == null)
                return;
            
            var playerRotationEuler = player.Rotation.Value.eulerAngles;
            playerRotationEuler.y += _playerInputService.PointerInput.x * _timeProvider.DeltaTime;
            var newRotation = Quaternion.Euler(playerRotationEuler);
            
            player.Rotation.SetValue(newRotation);
        }
    }
}