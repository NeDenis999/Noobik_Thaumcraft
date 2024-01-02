using System;
using System.Collections.Generic;
using UnityEngine;
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
        
        [Header("Drop")]
        public Transform DropContainer;
        [HideInInspector] public List<EntityBehaviour> DropItems;
        public int CountItems;
        
        [Header("Result")]
        public Transform ResultContainer;
        [HideInInspector] public List<EntityBehaviour> ResultItems;

        public EntityBehaviour ResultPrefab;
    }
}