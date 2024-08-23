using UnityEngine;

namespace Settings.Input.Impl
{
    [CreateAssetMenu(menuName = "Settings/"+ nameof(InputSettings), fileName = nameof(InputSettings))]
    public class InputSettings : ScriptableObject, 
        IInputSettings
    {
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _itemRotationSensitivity = 5f;

        public float CameraSensitivity => _sensitivity;

        public float ItemRotationSensitivity => _itemRotationSensitivity;
    }
}