using UnityEngine;

namespace Settings.Building
{
    public interface IBuildingSettings
    {
        LayerMask BuildingLayer { get; }
        float MagnetDistance { get; }
    }
}