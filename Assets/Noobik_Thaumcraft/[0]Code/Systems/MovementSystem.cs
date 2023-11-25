using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> _movableFilter;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(i);
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);

                ref var direction = ref directionComponent.Direction;
                ref var transform = ref modelComponent.ModelTransform;

                ref var characterController = ref movableComponent.CharacterController;
                ref var speed = ref movableComponent.Speed;

                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z);
                rawDirection = rawDirection.normalized;
                characterController.Move(rawDirection * speed * Time.deltaTime);
            }
        }
    }
}