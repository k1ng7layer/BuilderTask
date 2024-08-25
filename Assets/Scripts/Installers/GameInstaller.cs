using Repository;
using Services.Building.Impl;
using Services.Collision.Impl;
using Services.Input.Impl;
using Services.ItemPickup.Impl;
using Services.ItemSelection.Impl;
using Services.LevelProvider.Impl;
using Services.Spawn.Impl;
using Services.TimeProvider.Impl;
using Systems.Building;
using Systems.Camera;
using Systems.Core;
using Systems.Init;
using Systems.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneLevelData _sceneLevelData;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            BindSystems();
            BindServices();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnterBuildingModeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemMagnetSystem>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<UnityTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemSelectionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemPickupService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SurfaceCollisionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSurfaceProvider>().AsSingle();

            var levelSceneDataProvider = new SceneLevelDataProvider(_sceneLevelData);

            Container.BindInterfacesAndSelfTo<SceneLevelDataProvider>()
                .FromInstance(levelSceneDataProvider)
                .AsSingle();
        }
        
    }
}