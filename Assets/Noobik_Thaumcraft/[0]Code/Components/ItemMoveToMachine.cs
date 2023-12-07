using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public struct ItemMoveToMachine
    {
        public GameObject ItemGameObject;
        public Transform Target;
        public EcsEntity MachineEntity;
    }
}