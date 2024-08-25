using Entity;
using Helpers;
using Repository;
using Settings.Building;
using UnityEngine;

namespace Services.Building.Impl
{
    public class BuildingSurfaceProvider : IBuildingSurfaceProvider
    {
        private readonly CameraProvider _cameraProvider;
        private readonly IBuildingSettings _buildingSettings;

        public BuildingSurfaceProvider(
            CameraProvider cameraProvider, 
            IBuildingSettings buildingSettings
        )
        {
            _cameraProvider = cameraProvider;
            _buildingSettings = buildingSettings;
        }
        
        public bool TryGetValidSurface(ItemEntity itemEntity, out SurfaceInfo surface)
        {
            var camera = _cameraProvider.Camera;
            var dir = camera.Transform.Value.forward;
            surface = new SurfaceInfo();
            
            if (!Physics.Raycast(
                    camera.Transform.Value.position, 
                    dir, 
                    out var hit, 
                    _buildingSettings.MagnetDistance,
                    _buildingSettings.BuildingLayer))
                return false;
            
            if (hit.transform == null)
                return false;
                
            if (!hit.transform.gameObject.TryGetComponent<IBuildingSurface>(out var buildingSurface))
                return false;
                
            if (!itemEntity.AllowedSurface.Value.HasFlag(buildingSurface.BuildingSurfaceType))
                return false;

            surface = new SurfaceInfo(buildingSurface.Hash, hit.normal, hit.point);
            return true;
        }
    }
}