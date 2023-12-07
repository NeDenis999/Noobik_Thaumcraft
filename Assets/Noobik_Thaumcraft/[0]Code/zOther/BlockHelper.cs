using UnityEngine;

namespace Noobik_Thaumcraft
{
    public static class BlockHelper
    {
        public static Vector3 GetBlockInContainerPosition(in int itemsCount, in float distanceCell)
        {
            var number = itemsCount - 1;
            
            return new Vector3(
                -distanceCell * (number % 3),
                distanceCell * (number / 9 % 3),
                distanceCell * (number / 3 % 3));
        }
    }
}