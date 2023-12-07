using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class ItemMoveToSystem : IEcsRunSystem
    {
        private EcsFilter<ResultItemMoveToHero> _itemMoveFilter;
        private EcsFilter<HeroComponent, BackpackItemsComponent> _heroFilter;

        private EcsWorld _world;
        private SceneData _sceneData;
        private Configuration _config;
        private GameData _data;
        
        public void Run()
        {
            foreach (var itemMovedIndex in _itemMoveFilter)
            {
                var itemMovedEntity = _itemMoveFilter.GetEntity(itemMovedIndex);
                ref var itemComponent = ref _itemMoveFilter.Get1(itemMovedIndex);
                var itemTransform = itemComponent.ItemTransform;
                
                var target = _heroFilter.Get1(0).CharacterController.transform;
                
                var targetPosition =
                    BlockHelper.GetBlockInContainerPosition(1, _config.DistanceCell) + target.position;
                
                var distance = Vector3.Distance(itemTransform.position, targetPosition);
                
                if (distance > _config.DropDistance)
                {
                    itemTransform.position = Vector3.MoveTowards(itemTransform.position, targetPosition,  _config.MagnetizationSpeed * Time.deltaTime);
                }
                else
                {
                    ref var heroEntity = ref _heroFilter.GetEntity(0);
                    ref var backpack = ref heroEntity.Get<BackpackItemsComponent>();
                    ref var itemContainer = ref heroEntity.Get<ItemContainerComponent>();
                    
                    itemTransform.SetParent(itemContainer.Container);
                    var itemEntityBehaviour = itemTransform.GetComponent<EntityBehaviour>();
                    backpack.Items.Add(itemEntityBehaviour);

                    itemTransform.localPosition = 
                        BlockHelper.GetBlockInContainerPosition(backpack.Items.Count, _config.DistanceCell);
                    
                    itemTransform.eulerAngles = itemContainer.Container.eulerAngles;
                    
                    if (itemEntityBehaviour
                        .Entity.IsAlive() && itemEntityBehaviour
                        .Entity
                        .Has<PickItemComponent>() && itemEntityBehaviour
                        .Entity
                        .Get<PickItemComponent>()
                        .Animator)
                    itemEntityBehaviour
                        .Entity
                        .Get<PickItemComponent>()
                        .Animator.StopRotation();
                    
                    itemMovedEntity.Destroy();

                    //Обновление UI
                    _data.Diamonds += 1;

                    if (_data.Diamonds >= _config.TargetData[_data.IndexConfigTarget].Amount)
                    {
                        _data.IndexConfigTarget += 1;
                        _data.Diamonds = 0;
                    }

                    _sceneData.UI.TargetScreen.ViewUpgrade(_data.Diamonds, _config.TargetData[_data.IndexConfigTarget]);
                    _sceneData.UI.HeroProgressText.text = $"{backpack.Items.Count}/{_data.MaxItems}";
                }
            }
        }
    }
}