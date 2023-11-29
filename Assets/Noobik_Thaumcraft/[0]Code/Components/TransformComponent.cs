using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct TransformComponent
    {
        [FormerlySerializedAs("ModelTransform")]
        public Transform Transform;
    }
}