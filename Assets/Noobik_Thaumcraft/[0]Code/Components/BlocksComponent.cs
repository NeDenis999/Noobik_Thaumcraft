using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct BlocksComponent
    {
        [FormerlySerializedAs("Entities")]
        public List<EntityBehaviour> Entities;
    }
}