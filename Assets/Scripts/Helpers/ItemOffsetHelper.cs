using Entity;
using UnityEngine;

namespace Helpers
{
    public static class ItemOffsetHelper
    {
        public static Vector3 GetOffset(ItemEntity itemEntity)
        {
            var size = itemEntity.Size.Value;
            var offset = Vector3.zero;
            offset.z += size.z / 2f;
            offset.z += 1.5f;

            return offset;
        }
    }
}