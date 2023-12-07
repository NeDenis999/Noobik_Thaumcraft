using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningTrigger : MonoBehaviour
    {
        private readonly List<EcsEntity> _collisionEntities = new List<EcsEntity>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Block"))
                return;

            if (other.TryGetComponent<EntityBehaviour>(out var block))
            {
                if (block.Entity.Has<CanMining>())
                {
                    var newEntity = Startup.Instantiate.World.NewEntity();
                    newEntity.Get<TriggerBlockComponent>().Block = other.GetComponent<EntityBehaviour>();
            
                    _collisionEntities.Add(newEntity);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < _collisionEntities.Count; i++)
            {
                if (!_collisionEntities[i].IsAlive())
                {
                    _collisionEntities.Remove(_collisionEntities[i]);
                    continue;
                }
                
                if (other.gameObject == _collisionEntities[i].Get<TriggerBlockComponent>().Block.gameObject)
                {
                    _collisionEntities[i].Destroy();
                    _collisionEntities.Remove(_collisionEntities[i]);
                }
            }
        }
    }
}