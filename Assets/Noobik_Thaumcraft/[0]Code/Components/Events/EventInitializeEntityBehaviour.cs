using System;
using UnityEngine.Serialization;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct EventInitializeEntityBehaviour
    {
        [FormerlySerializedAs("EntityReference")]
        public EntityBehaviour entityBehaviour;
    }
}