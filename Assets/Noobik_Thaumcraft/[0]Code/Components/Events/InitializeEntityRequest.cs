using System;
using UnityEngine.Serialization;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct InitializeEntityRequest
    {
        [FormerlySerializedAs("EntityReference")]
        public EntityBehaviour entityBehaviour;
    }
}