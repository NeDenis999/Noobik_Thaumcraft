using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningZone : MonoBehaviour
    {
        [SerializeField]
        private EntityReference _hero;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Block")
                return;

            ref var blocks = ref _hero.Entity.Get<BlocksComponent>();

            if (blocks.References == null)
                blocks.References = new List<EntityReference>();
            
            blocks.References.Add(other.GetComponent<EntityReference>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Block")
                return;
            
            if (_hero.Entity.Has<BlocksComponent>())
                _hero.Entity.Get<BlocksComponent>().References.Remove(other.GetComponent<EntityReference>());
        }
    }
}