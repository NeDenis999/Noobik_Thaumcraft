using DG.Tweening;
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
                //выбор ближайшего предмета
                var resultIndex = GetNearestItemIndex(heroIndex);
                if (resultIndex == -1) 
                    continue;

                //создание анимации движенияn
                var item = _triggerPickUp.Get1(resultIndex).PickUpItem;
                ref var eventComponent = ref _world.NewEntity().Get<EventItemStartMove>();
                eventComponent.Item = item;
                eventComponent.Target = _heroFilter.Get1(heroIndex).CharacterController.GetComponent<EntityBehaviour>();
                
                //Настройка подобранного предмета
                var triggerEntity = _triggerPickUp.GetEntity(resultIndex);

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

                //Удаление триггера
                triggerEntity.Destroy();
            }
        }

        private int GetNearestItemIndex(int heroIndex)
        {
            float minDistance = float.MaxValue;
            int resultIndex = -1;

            var heroTransform = _heroFilter.Get2(heroIndex).Transform;
            var heroPosition = heroTransform.position;

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

            return resultIndex;
        }
    }
}