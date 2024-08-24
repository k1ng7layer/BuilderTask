using System;
using Helpers;

namespace Services.Items
{
    public interface IItemSettingsBase
    {
        //public List<ItemSettings> SettingsList 
    }

    [Serializable]
    public class ItemSettings
    {
        public string PrefabName;
        public BuildingSurfaceType BuildingSurfaceType;
    }
}