using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickingZone : MonoBehaviour
    {
        [SerializeField]
        private EntityBehaviour _hero;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "PickItem")
                return;

            if (_hero.Entity.Has<BreakTag>())
                return;
            
            ref var items = ref _hero.Entity.Get<PickItemsCollectionComponent>();

            if (items.Entities == null)
                items.Entities = new List<EntityBehaviour>();
            
            items.Entities.Add(other.GetComponent<EntityBehaviour>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "PickItem")
                return;
            
            if (_hero.Entity.Has<PickItemsCollectionComponent>())
                _hero.Entity.Get<PickItemsCollectionComponent>().Entities.Remove(other.GetComponent<EntityBehaviour>());
        }
    }
}