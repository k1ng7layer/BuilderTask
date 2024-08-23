using System.Collections.Generic;
using UnityEngine;
using Views;
using Views.Impl;

namespace Services.LevelProvider.Impl
{
    public class SceneLevelData : MonoBehaviour, 
        ILevelData
    {
        [SerializeField] private Transform _playerInitTransform;
        [SerializeField] private List<ItemView> _itemViews;

        private List<IEntityView> _entityViews;

        public Vector3 PlayerSpawnPosition => _playerInitTransform.position;
        public Quaternion PlayerSpawnRotation => _playerInitTransform.rotation;

        public List<IEntityView> ItemsViews
        {
            get
            {
                if (_entityViews == null)
                {
                    _entityViews = new List<IEntityView>();
                    _entityViews.AddRange(_itemViews);
                }

                return _entityViews;
            }
        }
    }
}