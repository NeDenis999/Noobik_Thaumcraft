using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct RotateComponent
    {
        public float Speed;
        public Transform Transform;
        public Quaternion Quaternion;
    }
}