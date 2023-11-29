using System;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct HeroComponent
    {
        [FormerlySerializedAs("HeroAnimator")]
        public HeroAnimator Animator;
        public CharacterController CharacterController;
        public float Speed;
    }
}