using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickingZone : MonoBehaviour
    {
        [SerializeField]
        private EntityReference _hero;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "PickItem")
                return;

            if (_hero.Entity.Has<BreakTag>())
                return;
            
            ref var items = ref _hero.Entity.Get<PickItemsColectionComponent>();

            if (items.References == null)
                items.References = new List<EntityReference>();
            
            items.References.Add(other.GetComponent<EntityReference>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "PickItem")
                return;
            
            if (_hero.Entity.Has<PickItemsColectionComponent>())
                _hero.Entity.Get<PickItemsColectionComponent>().References.Remove(other.GetComponent<EntityReference>());
        }
    }
}