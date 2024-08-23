using UnityEngine;

namespace Settings.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/"+ nameof(PlayerSettings), fileName = nameof(PlayerSettings))]
    public class PlayerSettings : ScriptableObject, 
        IPlayerSettings
    {
        [SerializeField] private float moveSpeed = 4f;

        public float MoveSpeed => moveSpeed;
    }
}