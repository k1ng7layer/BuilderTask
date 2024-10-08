﻿using UnityEngine;

namespace Settings.Building
{
    [CreateAssetMenu(menuName = "Settings/"+ nameof(BuildingSettings), fileName = nameof(BuildingSettings))]
    public class BuildingSettings : ScriptableObject, 
        IBuildingSettings
    {
        [SerializeField] private float _magnetDistance = 10f;
        [SerializeField] private LayerMask _buildingLayer;
        [SerializeField] private float _rotationDeltaDeg = 45f;

        public LayerMask BuildingLayer => _buildingLayer;

        public float MagnetDistance => _magnetDistance;
        public float RotationDeltaDeg => _rotationDeltaDeg;       
    }
}