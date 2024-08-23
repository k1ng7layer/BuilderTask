using Settings.Building;
using Settings.Input.Impl;
using Settings.Player.Impl;
using Settings.Prefabs;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Settings/Installers/"+ nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private InputSettings _inputSettings;
        [SerializeField] private PrefabBase _prefabBase;
        [SerializeField] private BuildingSettings _buildingSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerSettings>().FromInstance(_playerSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<InputSettings>().FromInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<PrefabBase>().FromInstance(_prefabBase).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSettings>().FromInstance(_buildingSettings).AsSingle();
        }
    }
}