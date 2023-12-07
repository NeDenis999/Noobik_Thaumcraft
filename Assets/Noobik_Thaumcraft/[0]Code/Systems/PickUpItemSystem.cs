using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickUpItemSystem : IEcsRunSystem
    {
        private EcsFilter<HeroComponent, TransformComponent, ItemContainerComponent, BackpackItemsComponent>
            .Exclude<TimerNotPickUpBlockComponent> _heroFilter;

        private EcsFilter<TimerNotPickUpBlockComponent> _notPickUpBlockFilter;
        private EcsFilter<TriggerPickUpItemComponent> _triggerPickUp;

        private EcsWorld _world;
        private SaveLoadService _saveLoad;
        private GameData _data;
        private SceneData _sceneData;
        private Configuration _config;

        public void Run()
        {
            foreach (var notPickUpIndex in _notPickUpBlockFilter)
            {
                ref var timer = ref _notPickUpBlockFilter.Get1(notPickUpIndex);

                if (timer.Timer > 0)
                    timer.Timer -= Time.deltaTime;
                else
                    _notPickUpBlockFilter.GetEntity(notPickUpIndex).Del<TimerNotPickUpBlockComponent>();
            }

            foreach (var heroIndex in _heroFilter)
            {
                float minDistance = float.MaxValue;
                int resultIndex = -1;

                var heroPosition = _heroFilter.Get2(heroIndex).Transform.position;

                foreach (var pickUpIndex in _triggerPickUp)
                {
                    var pickUpTransform = _triggerPickUp.Get1(pickUpIndex).PickUpItem.transform;
                    var pickUpPosition = pickUpTransform.position;
                    var distance = Vector3.Distance(pickUpPosition, heroPosition);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        resultIndex = pickUpIndex;
                    }
                }

                if (resultIndex == -1)
                    continue;

                //создаём сущность движущегося айтема
                ref var itemMovedComponent = ref _world.NewEntity().Get<ResultItemMoveToHero>();
                var item = _triggerPickUp.Get1(resultIndex).PickUpItem;
                var triggerEntity = _triggerPickUp.GetEntity(resultIndex);
                itemMovedComponent.ItemTransform = item.transform;

                var pickUpBehaviour = _triggerPickUp.Get1(resultIndex).PickUpItem;
                var pickUpEntity = pickUpBehaviour.Entity;

                if (pickUpEntity.Has<PickItemComponent>())
                {
                    ref var pickItemComponent = ref pickUpEntity.Get<PickItemComponent>();
                    Object.Destroy(pickItemComponent.Collider);
                    Object.Destroy(pickItemComponent.Rigidbody);
                }

                if (pickUpEntity.Has<BlockComponent>())
                {
                    ref var block = ref pickUpEntity.Get<BlockComponent>();
                    block.View.PlayStay();
                }

                triggerEntity.Destroy();
            }
        }
    }
}