using System;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct MachineComponent
    {
        public float CreateTime;
        public ResourceType IncomingResource;
        public ResourceType ResultResource;
        public Slider Slider;
    }
}