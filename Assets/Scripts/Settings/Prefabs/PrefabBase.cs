using System;
using UnityEngine;

namespace Settings.Prefabs
{
    [CreateAssetMenu(menuName = "Settings/"+ nameof(PrefabBase), fileName = nameof(PrefabBase))]
    public class PrefabBase : ScriptableObject
    {
        [SerializeField] private PrefabSettings[] _settings;
        public GameObject Get(string prefabName)
        {
            foreach (var setting in _settings)
            {
                if (setting.Name == prefabName)
                    return setting.Prefab;
            }

            throw new Exception($"[{nameof(PrefabBase)}] can't find prefab with name {prefabName}");
        }
    }

    [Serializable]
    class PrefabSettings
    {
        public string Name;
        public GameObject Prefab;
    }
}