using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<HeroComponent, TransformComponent, DirectionComponent> _movableFilter;

        private float _currentSpeed;
        private float _recoveryRate = 3f;
        
        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var heroComponent = ref _movableFilter.Get1(i);
                ref var transformComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);

                ref var direction = ref directionComponent.Direction;
                ref var transform = ref transformComponent.Transform;

                ref var characterController = ref heroComponent.CharacterController;
                ref var speed = ref heroComponent.Speed;
                ref var animator = ref heroComponent.Animator;

                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z);
                rawDirection = rawDirection.normalized;
                characterController.Move(rawDirection * speed * Time.deltaTime);
                
                _currentSpeed =
                    Mathf.MoveTowards(_currentSpeed, rawDirection.magnitude, _recoveryRate * Time.deltaTime);
                animator.SetSpeed(_currentSpeed);
            }
        }
    }
}