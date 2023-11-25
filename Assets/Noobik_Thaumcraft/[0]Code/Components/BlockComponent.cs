using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct BlockComponent
    {
        public GameObject GameObject;
        public float TimeDestroy;
        public ResourceType ResourceType;
        public BlockAnimator View;
        public Rigidbody Rigidbody;
    }
}