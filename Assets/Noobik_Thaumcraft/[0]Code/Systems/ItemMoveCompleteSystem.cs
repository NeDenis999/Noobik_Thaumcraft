using System;
using Leopotam.Ecs;
using Object = UnityEngine.Object;

namespace Noobik_Thaumcraft
{
    public class ItemMoveCompleteSystem : IEcsRunSystem
    {
        private EcsFilter<EventItemMoveToHeroComplete>.Exclude<ProcessNotOver> _moveToHeroComplete;
        private EcsFilter<EventItemMoveToMachineComplete>.Exclude<ProcessNotOver> _moveToMachineComplete;
        private EcsFilter<EventItemMoveToTrashcanComplete>.Exclude<ProcessNotOver> _moveToTrashcanComplete;
        private EcsFilter<HeroComponent, BackpackItemsComponent> _heroFilter;
        
        private EcsWorld _world;
        private SceneData _sceneData;
        private Configuration _config;
        private GameData _data;
        
        public void Run()
        {
            foreach (var index in _moveToHeroComplete)
            {
                ref var heroEntity = ref _heroFilter.GetEntity(0);
                ref var backpack = ref heroEntity.Get<BackpackItemsComponent>();
                var itemTransform = _moveToHeroComplete.Get1(index).Item.transform;

                var itemEntityBehaviour = itemTransform.GetComponent<EntityBehaviour>();
                ref var itemEntity = ref itemEntityBehaviour.Entity;

                if (itemEntity.IsAlive() 
                    && itemEntity.Has<PickItemComponent>() 
                    && itemEntity.Get<PickItemComponent>().Animator) 
                    itemEntity
                        .Get<PickItemComponent>()
                        .Animator.StopRotation();

                //Обновление UI
                _data.Diamonds += 1;

                if (_data.Diamonds >= _config.TargetData[_data.IndexConfigTarget].Amount)
                {
                    _data.IndexConfigTarget += 1;
                    _data.Diamonds = 0;
                }

                _sceneData.UI.TargetScreen.ViewUpgrade(_data.Diamonds, _config.TargetData[_data.IndexConfigTarget]);
                _sceneData.UI.CountItemsLabel.ViewUpdate(backpack.Items.Count, _data.MaxItems);
                
                _moveToHeroComplete.GetEntity(index).Destroy();
            }
            
            foreach (var index in _moveToMachineComplete)
            {
                ref var heroEntity = ref _heroFilter.GetEntity(0);
                ref var backpack = ref heroEntity.Get<BackpackItemsComponent>();
                ref var eventComponent = ref _moveToMachineComplete.Get1(index);
                var itemEntity = eventComponent.Item;

                ref var machineEntity = ref eventComponent.Machine.Entity;
                ref var machineComponent = ref machineEntity.Get<MachineComponent>();
                
                machineComponent.DropItems.Add(itemEntity);

                //Обновление UI
                _sceneData.UI.CountItemsLabel.ViewUpdate(backpack.Items.Count, _data.MaxItems);
                
                //Удаление
                _moveToMachineComplete.GetEntity(index).Destroy();
            }
            
            foreach (var index in _moveToTrashcanComplete)
            {
                ref var heroEntity = ref _heroFilter.GetEntity(0);
                ref var backpack = ref heroEntity.Get<BackpackItemsComponent>();
                ref var eventComponent = ref _moveToTrashcanComplete.Get1(index);
                var itemEntity = eventComponent.Item;

                //Удаление предмета
                itemEntity.Entity.Destroy();
                Object.Destroy(itemEntity.gameObject);

                //Обновление UI
                _sceneData.UI.CountItemsLabel.ViewUpdate(backpack.Items.Count, _data.MaxItems);
                
                //Удаление сущности движения
                _moveToTrashcanComplete.GetEntity(index).Destroy();
            }
        }
    }
}