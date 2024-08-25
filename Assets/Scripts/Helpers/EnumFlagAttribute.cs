using UnityEngine;

namespace Helpers
{
    public class EnumFlagAttribute : PropertyAttribute
    {
        public string name;

        public EnumFlagAttribute() { }

        public EnumFlagAttribute(string name)
        {
            this.name = name;
        }
    }
}