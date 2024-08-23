using System.Collections.Generic;
using UnityEngine;
using Views;

namespace Services.LevelProvider
{
    public interface ILevelData
    {
        Vector3 PlayerSpawnPosition { get;}
        Quaternion PlayerSpawnRotation { get;}
        List<IEntityView> ItemsViews { get; }
    }
}