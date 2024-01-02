using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TrashcanTrigger : MonoBehaviour
    {
        [SerializeField] 
        private EntityBehaviour _trashcanEntity;
        
        private EcsEntity _triggerEntity;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            var heroEntityBehaviour = other.GetComponent<EntityBehaviour>();
            
            if (heroEntityBehaviour)
            {
                if (heroEntityBehaviour.Entity.Has<BackpackItemsComponent>() 
                    && heroEntityBehaviour.Entity.Get<BackpackItemsComponent>().Items.Count != 0)
                {
                    if (_triggerEntity.IsAlive())
                        _triggerEntity.Destroy();
                    
                    var newEntity = Startup.Instantiate.World.NewEntity();
                    newEntity.Get<EventDropTriggeredTrashcan>().Trashcan = _trashcanEntity;
                    _triggerEntity = newEntity;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (_triggerEntity.IsAlive())
                _triggerEntity.Destroy();
        }
    }
}