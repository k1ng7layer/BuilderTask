using UnityEngine;
using Views;

namespace Services.Spawn
{
    public interface ISpawnService
    {
        IEntityView Spawn(string prefabName, Vector3 position, Quaternion rotation);
    }
}