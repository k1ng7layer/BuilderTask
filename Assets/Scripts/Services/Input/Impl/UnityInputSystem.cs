using System;
using Settings.Input;
using UnityEngine;
using Zenject;

namespace Services.Input.Impl
{
    public class UnityInputSystem : IPlayerInputService, 
        ITickable
    {
        private readonly IInputSettings _inputSettings;

        public UnityInputSystem(IInputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }
        
        public Vector3 Input { get; private set; }
        public Vector2 PointerInput { get; private set; }
        public event Action UseButtonClicked;
        public event Action<float> ItemRotation;

        public void Tick()
        {
            var mouseX = UnityEngine.Input.GetAxis("Mouse X");
            var mouseY = UnityEngine.Input.GetAxis("Mouse Y");
            PointerInput = new Vector2(mouseX * _inputSettings.CameraSensitivity, 
                mouseY * _inputSettings.CameraSensitivity);
            
            var x = UnityEngine.Input.GetAxisRaw("Horizontal");
            var y = UnityEngine.Input.GetAxisRaw("Vertical");
            
            Input = new Vector3(x, 0f, y);
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
                UseButtonClicked?.Invoke();

            var scrollWheel = UnityEngine.Input.GetAxis("Mouse ScrollWheel");

            if (scrollWheel > 0 || scrollWheel < 0)
            {
                var value = scrollWheel > 0 ? 1 : -1;
                ItemRotation?.Invoke(value);
            }
        }
    }
}