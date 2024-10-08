﻿using System.Collections.Generic;
using Entity;
using Repository;
using Services.LevelProvider;
using Services.Spawn;
using Systems.Core;
using UnityEngine;
using Views.Impl;

namespace Systems.Init
{
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly ISpawnService _spawnService;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly ItemProvider _itemProvider;
        private readonly CameraProvider _cameraProvider;

        public GameInitializeSystem(
            ISpawnService spawnService, 
            ILevelDataProvider levelDataProvider, 
            PlayerProvider playerProvider,
            ItemProvider itemProvider,
            CameraProvider cameraProvider
        )
        {
            _spawnService = spawnService;
            _levelDataProvider = levelDataProvider;
            _playerProvider = playerProvider;
            _itemProvider = itemProvider;
            _cameraProvider = cameraProvider;
        }
        
        public void Initialize()
        {
            var playerView = InitPlayer();
            InitCamera(playerView);
            InitItems();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private PlayerEntity InitPlayer()
        {
            var levelData = _levelDataProvider.LevelData;
            var playerView = _spawnService.Spawn("Player", 
                levelData.PlayerSpawnPosition, 
                levelData.PlayerSpawnRotation);
            
            var player = new PlayerEntity();
            player.Position.SetValue(levelData.PlayerSpawnPosition);
            player.Rotation.SetValue( levelData.PlayerSpawnRotation);
            player.Transform.SetValue( playerView.Transform);
            
            playerView.LinkEntity(player);
            
            _playerProvider.Set(player);

            return player;
        }

        private void InitCamera(PlayerEntity target)
        {
            var cameraView = _spawnService.Spawn("Camera", Vector3.zero, Quaternion.identity);
            var camera = new CameraEntity();
            cameraView.LinkEntity(camera);
            
            camera.Parent.SetValue(target.ItemPlaceholderTransform.Value);
            camera.LocalPosition.SetValue(new Vector3(0f, .2f, 0f));
            camera.Rotation.SetValue(Quaternion.identity);
            camera.Transform.SetValue(cameraView.Transform);
            
            _cameraProvider.SetCamera(camera);
        }

        private void InitItems()
        {
            var levelData = _levelDataProvider.LevelData;

            foreach (var itemView in levelData.ItemsViews)
            {
                var view = (ItemView)itemView;
                var item = new ItemEntity();
                item.Position.SetValue(itemView.Transform.position);
                item.Rotation.SetValue(itemView.Transform.rotation);
                item.AllowedSurface.SetValue(view.AllowedSurface);
                item.BuildingEntityType.SetValue(view.BuildingSurfaceType);
                item.Transform.SetValue(itemView.Transform);
                item.Collisions.SetValue(new HashSet<int>());
                item.Layer.SetValue(LayerMask.NameToLayer("BuildingSurface"));
                
                itemView.LinkEntity(item);
                
                _itemProvider.AddItem(itemView.Transform.GetHashCode(), item);
            }
        }
    }
}