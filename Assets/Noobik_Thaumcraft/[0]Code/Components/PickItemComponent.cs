using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct PickItemComponent
    {
        public ResourceType Type;
        public Rigidbody Rigidbody;
        public Collider Collider;
        public GameObject GameObject;
        public PickItemAnimator Animator;
    }
}