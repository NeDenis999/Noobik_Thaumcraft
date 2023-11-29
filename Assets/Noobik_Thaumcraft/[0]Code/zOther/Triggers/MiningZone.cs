using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningZone : MonoBehaviour
    {
        [SerializeField]
        private EntityBehaviour _hero;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Block")
                return;

            ref var blocks = ref _hero.Entity.Get<BlocksComponent>();

            if (blocks.Entities == null)
                blocks.Entities = new List<EntityBehaviour>();
            
            blocks.Entities.Add(other.GetComponent<EntityBehaviour>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Block")
                return;
            
            if (_hero.Entity.Has<BlocksComponent>())
                _hero.Entity.Get<BlocksComponent>().Entities.Remove(other.GetComponent<EntityBehaviour>());
        }
    }
}