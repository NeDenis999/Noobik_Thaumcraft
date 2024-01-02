using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class ItemStartMoveSystem : IEcsRunSystem
    {
        private EcsFilter<EventItemStartMove> _moveStartFilter;
        private EcsFilter<HeroComponent, BackpackItemsComponent> _heroFilter;
        
        private EcsWorld _world;
        private SceneData _sceneData;
        private Configuration _config;
        private GameData _data;
        
        public void Run()
        {
            foreach (var index in _moveStartFilter)
            {
                ref var startMoveComponent = ref _moveStartFilter.Get1(index);

                if (startMoveComponent.Target.Entity.Has<HeroComponent>())
                {
                    ref var heroEntity = ref startMoveComponent.Target.Entity;
                    ref var backpack = ref heroEntity.Get<BackpackItemsComponent>();
                    ref var itemContainer = ref heroEntity.Get<ItemContainerComponent>();
                    var itemTransform = startMoveComponent.Item.transform;
                    var itemEntityBehaviour = itemTransform.GetComponent<EntityBehaviour>();
                    
                    itemTransform.SetParent(itemContainer.Container);
                    itemTransform.eulerAngles = itemContainer.Container.eulerAngles;
                    backpack.Items.Add(itemEntityBehaviour);
                    
                    var eventEntity = _world.NewEntity();
                    eventEntity.Get<EventItemMoveToHeroComplete>().Item = backpack.Items[backpack.Items.Count - 1];
                    eventEntity.Get<ProcessNotOver>();
                    
                    itemTransform.DOLocalJump(
                        BlockHelper.GetBlockInContainerPosition(backpack.Items.Count + 1, _config.DistanceCell),
                        1.2f, 1, _config.ItemJumpDuration).OnComplete(
                        () =>
                        {
                            eventEntity.Del<ProcessNotOver>();
                        });
                    
                    _moveStartFilter.GetEntity(index).Destroy();
                    continue;
                }

                if (startMoveComponent.Target.Entity.Has<MachineComponent>())
                {
                    ref var machineEntity = ref startMoveComponent.Target.Entity;
                    ref var machineComponent = ref machineEntity.Get<MachineComponent>();
                    var itemContainer = machineComponent.DropContainer;

                    var itemTransform = startMoveComponent.Item.transform;
                    itemTransform.SetParent(itemContainer);
                    itemTransform.eulerAngles = itemContainer.eulerAngles;
                    
                    var eventMoveCompleteEntity = _world.NewEntity();
                    ref var eventMoveComplete = ref eventMoveCompleteEntity.Get<EventItemMoveToMachineComplete>();
                    eventMoveComplete.Item = startMoveComponent.Item;
                    eventMoveComplete.Machine = startMoveComponent.Target;
                    eventMoveCompleteEntity.Get<ProcessNotOver>();

                    machineComponent.CountItems += 1;
                    
                    itemTransform.DOLocalJump(
                        BlockHelper.GetBlockInContainerPosition(machineComponent.CountItems + 1, _config.DistanceCell),
                        1.2f, 1, _config.ItemJumpDuration).OnComplete(
                        () =>
                        { 
                            eventMoveCompleteEntity.Del<ProcessNotOver>();
                        });
                    
                    _moveStartFilter.GetEntity(index).Destroy();
                    continue;
                }

                if (startMoveComponent.Target.Entity.Has<TrashcanComponent>())
                {
                    ref var trashcanEntity = ref startMoveComponent.Target.Entity;
                    ref var trashcanComponent = ref trashcanEntity.Get<TrashcanComponent>();
                    var container = trashcanComponent.Transform;
                    
                    var itemTransform = startMoveComponent.Item.transform;
                    itemTransform.SetParent(container);
                    itemTransform.eulerAngles = container.eulerAngles;
                    
                    var eventMoveCompleteEntity = _world.NewEntity();
                    ref var moveComplete = ref eventMoveCompleteEntity.Get<EventItemMoveToTrashcanComplete>();
                    moveComplete.Item = startMoveComponent.Item;
                    eventMoveCompleteEntity.Get<ProcessNotOver>();
                    
                    itemTransform.DOLocalJump(
                        Vector3.zero, 1.2f, 1, _config.ItemJumpDuration).OnComplete(
                        () =>
                        { 
                            eventMoveCompleteEntity.Del<ProcessNotOver>();
                        });
                    
                    _moveStartFilter.GetEntity(index).Destroy();
                    continue;
                }

                Debug.Log("Для этого таргета не написана логика");
            }
        }
    }
}