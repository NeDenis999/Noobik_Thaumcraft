using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickUpItemSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeroTag, ModelComponent, PickItemsColectionComponent, ItemContainerComponent> _heroFilter;
        private readonly SaveLoadService _saveLoad;
        
        public void Run()
        {
            foreach (var index in _heroFilter)
            {
                ref var itemsComponent = ref _heroFilter.Get3(index);

                if (itemsComponent.References == null)
                    continue;
                
                ref var transform = ref _heroFilter.Get2(index).ModelTransform;

                float minDistance = float.MaxValue;
                EntityReference result = null;
                
                foreach (var reference in itemsComponent.References)
                {
                    /*if (!reference.Entity.Has<BreakTag>())
                        continue;*/

                    ref var item = ref reference.Entity.Get<PickItemComponent>();
                    var distance = Vector3.Distance(item.Collider.transform.position, transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        result = reference;
                    }
                }

                if (result != null)
                {
                    ref var timer = ref result.Entity.Get<PickUpDuration>();
                    
                    if (timer.Timer > 0)
                    {
                        timer.Timer -= Time.deltaTime;
                        continue;
                    }
                    
                    itemsComponent.References.Remove(result);
                    
                    var entityItemResult = result.Entity;

                    if (entityItemResult.Has<BlockComponent>())
                        entityItemResult.Get<BlockComponent>().View.PlayStay();

                    ref var pickItem = ref entityItemResult.Get<PickItemComponent>();
                    ref var container = ref _heroFilter.Get4(index);

                    var heroEntity = _heroFilter.GetEntity(index);
                    ref var backpackItems = ref heroEntity.Get<BackpackItemsComponent>();

                    if (backpackItems.References == null)
                        backpackItems.References = new List<EntityReference>();
                    
                    backpackItems.References.Add(result);
                    
                    pickItem.Collider.transform.SetParent(container.Container);
                    int number = backpackItems.References.Count - 1;
                    float distance = 0.5f;
                    pickItem.Collider.transform.localPosition = 
                        new Vector3(-distance * (number % 3), distance * (number / 9 % 3), distance * (number / 3 % 3));
                    
                    pickItem.Collider.transform.eulerAngles = container.Container.eulerAngles;
                    
                    Object.Destroy(pickItem.Rigidbody);
                    Object.Destroy(pickItem.Collider);

                    _saveLoad.DiamondStorage.Add(1);
                }
            }
        }
    }
}