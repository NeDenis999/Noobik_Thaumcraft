using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public sealed class RotateAtMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, DirectionComponent, RotateComponent, MoveEvent> _rotateFilter = null;

        public void Run()
        {
            foreach (var i in _rotateFilter)
            {
                ref var modelComponent = ref _rotateFilter.Get1(i);
                ref var directionComponent = ref _rotateFilter.Get2(i);
                ref var rotateComponent = ref _rotateFilter.Get3(i);

                ref var direction = ref directionComponent.Direction;
                ref var transform = ref rotateComponent.Transform;
                ref var speed = ref rotateComponent.Speed;
                ref var rotation = ref rotateComponent.Quaternion;

                Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);
                rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
            }
        }
    }
}