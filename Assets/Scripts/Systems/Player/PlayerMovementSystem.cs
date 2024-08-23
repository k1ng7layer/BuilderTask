using Repository;
using Services.Input;
using Services.TimeProvider;
using Settings.Player;
using Systems.Core;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerMovementSystem : IUpdateSystem
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly IPlayerSettings _playerSettings;
        private readonly ITimeProvider _timeProvider;
        private readonly PlayerProvider _playerProvider;

        public PlayerMovementSystem(
            IPlayerInputService playerInputService,
            IPlayerSettings playerSettings,
            ITimeProvider timeProvider,
            PlayerProvider playerProvider
        )
        {
            _playerInputService = playerInputService;
            _playerSettings = playerSettings;
            _timeProvider = timeProvider;
            _playerProvider = playerProvider;
        }
        
        public void Update()
        {
            var player = _playerProvider.Player;
            
            if (player == null)
                return;
            
            var input = _playerInputService.Input.normalized;
            var forward = player.Rotation.Value * Vector3.forward;
            var right = player.Rotation.Value * Vector3.right;
            var movementDir = forward * input.z + right * input.x;
            var movementDelta = movementDir * _timeProvider.DeltaTime * _playerSettings.MoveSpeed;
            var playerPosition = player.Position.Value;
            var newPosition = playerPosition + movementDelta;
            
            player.Position.SetValue(movementDelta);
        }
    }
}