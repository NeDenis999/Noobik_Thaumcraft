using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MachineDropItemSystem : IEcsRunSystem
    {
        private EcsFilter<TimerNotCanDropComponent> _notDropFilter;
        private EcsFilter<HeroComponent, TransformComponent, BackpackItemsComponent>.Exclude<TimerNotCanDropComponent> _heroFilter;
        private EcsFilter<EventDropTriggeredMachine> _eventTriggeredFilter;
        private EcsFilter<ItemMoveToMachine> _itemMovedFilter;

        private EcsWorld _world;
        private GameData _data;
        private Configuration _config;

        public void Run()
        {
            foreach (var notDropIndex in _notDropFilter)
            {
                ref var timer = ref _notDropFilter.Get1(notDropIndex);

                if (timer.Timer > 0)
                    timer.Timer -= Time.deltaTime;
                else
                    _notDropFilter.GetEntity(notDropIndex).Del<TimerNotCanDropComponent>();
            }
            
            foreach (var triggerIndex in _eventTriggeredFilter)
            {
                foreach (var heroIndex in _heroFilter)
                {
                    var heroEntity = _heroFilter.GetEntity(heroIndex);
                    ref var backpackComponent = ref _heroFilter.Get3(0);
                    var items = backpackComponent.Items;
                
                    if (items.Count == 0)
                        continue;
                
                    var item = items[items.Count - 1];
                    var itemTransform = item.transform;
                    ref var machineComponent = ref _eventTriggeredFilter.Get1(triggerIndex).Machine.Entity.Get<MachineComponent>();
                    var dropContainer = machineComponent.DropContainer;

                    ref var itemMovedComponent = ref _world.NewEntity().Get<ItemMoveToMachine>();
                    itemMovedComponent.Target = dropContainer;
                    itemMovedComponent.ItemGameObject = item.gameObject;
                    itemMovedComponent.MachineEntity = _eventTriggeredFilter.Get1(triggerIndex).Machine.Entity;
                    machineComponent.Slider.value = 0;
                    
                    itemTransform.SetParent(dropContainer);
                    items.Remove(item);

                    heroEntity.Get<TimerNotCanDropComponent>().Timer = _config.NotDropTime;
                }
            }

            foreach (var itemMovedIndex in _itemMovedFilter)
            {
                var itemMovedEntity = _itemMovedFilter.GetEntity(itemMovedIndex);
                ref var itemComponent = ref _itemMovedFilter.Get1(itemMovedIndex);
                var itemTransform = itemComponent.ItemGameObject.transform;
                var target = itemComponent.Target;
                
                var distance = Vector3.Distance(itemTransform.position, target.position);
                
                if (distance > _config.DropDistance)
                {
                    itemTransform.position = Vector3.MoveTowards(itemTransform.position, target.position,  _config.MagnetizationSpeed * Time.deltaTime);
                }
                else
                {
                    ref var machineComponent = ref itemComponent.MachineEntity.Get<MachineComponent>();
                    machineComponent.DropItems.Add(itemComponent.ItemGameObject.GetComponent<EntityBehaviour>());

                    itemTransform.localPosition = 
                        BlockHelper.GetBlockInContainerPosition(machineComponent.DropItems.Count, _config.DistanceCell);
                    
                    itemTransform.eulerAngles = Vector3.zero;
                    itemMovedEntity.Destroy();
                }
            }
        }
    }
}