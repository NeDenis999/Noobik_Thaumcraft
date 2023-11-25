using System;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct HideAtDistanceComponent
    {
        public float MinDistance;
        public float MaxDistance;
        public Image Image;
    }
}