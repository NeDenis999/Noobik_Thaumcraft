using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickingTrigger : MonoBehaviour
    {
        private readonly List<EcsEntity> _collisionEntities = new List<EcsEntity>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("PickItem"))
                return;

            if (other.TryGetComponent<EntityBehaviour>(out var block))
            {
                if (block.Entity.Has<CanPickUpComponent>())
                {
                    var newEntity = Startup.Instantiate.World.NewEntity();
                    newEntity.Get<TriggerPickUpItemComponent>().PickUpItem = other.GetComponent<EntityBehaviour>();
            
                    _collisionEntities.Add(newEntity);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("PickItem"))
                return;

            for (int i = 0; i < _collisionEntities.Count; i++)
            {
                if (!_collisionEntities[i].IsAlive())
                {
                    _collisionEntities.Remove(_collisionEntities[i]);
                    continue;
                }
                
                if (other.gameObject == _collisionEntities[i].Get<TriggerPickUpItemComponent>().PickUpItem.gameObject)
                {
                    _collisionEntities[i].Destroy();
                    _collisionEntities.Remove(_collisionEntities[i]);
                }
            }
        }
    }
}