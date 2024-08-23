using Repository;
using Services.Input;
using Services.TimeProvider;
using Systems.Core;
using UnityEngine;

namespace Systems.Camera
{
    public class CameraRotationSystem : IUpdateSystem
    {
        private readonly IPlayerInputService _playerInputService;
        private readonly ITimeProvider _timeProvider;
        private readonly CameraProvider _cameraProvider;
        private float _cameraRotation;

        public CameraRotationSystem(
            IPlayerInputService playerInputService, 
            ITimeProvider timeProvider,
            CameraProvider cameraProvider
        )
        {
            _playerInputService = playerInputService;
            _timeProvider = timeProvider;
            _cameraProvider = cameraProvider;
        }
        
        public void Update()
        {
            var camera = _cameraProvider.Camera;
            
            if (camera == null)
                return;
            
            var input = _playerInputService.PointerInput;
            var rotationEuler = camera.LocalRotation.Value.eulerAngles;
            var rotation = input.y * _timeProvider.DeltaTime;
            //rotationEuler.x -= rotation;

            _cameraRotation -= rotation;
            
            Debug.Log($"rotationEuler.x: {rotationEuler.x}");
            _cameraRotation = Mathf.Clamp(_cameraRotation, -90f, 90f);
            camera.Transform.Value.localEulerAngles = Vector3.right *_cameraRotation;
            // var newRotation = Quaternion.Euler(rotationEuler);
            // camera.LocalRotation.SetValue(newRotation);
        }
    }
}