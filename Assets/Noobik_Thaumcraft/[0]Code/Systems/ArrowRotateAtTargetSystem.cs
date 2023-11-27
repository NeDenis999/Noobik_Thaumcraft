using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class ArrowRotateAtTargetSystem : IEcsRunSystem
    {
        private readonly TargetsDataManager _targetsManager = null;
        private readonly TargetPointsContainer _targetPointsContainer = null;
        
        private readonly EcsFilter<ArrowTargetTag, RotateComponent> _rotateFilter = null;

        public void Run()
        {
            foreach (var i in _rotateFilter)
            {
                ref var rotateComponent = ref _rotateFilter.Get2(i);
                
                ref var transform = ref rotateComponent.Transform;
                ref var speed = ref rotateComponent.Speed;

                var targetPosition = _targetPointsContainer.GetPoint(_targetsManager.GetCurrentTarget().Location, transform)
                .position;
                var direction = (targetPosition - transform.position).normalized;
                
                Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);

                transform.eulerAngles = transform.eulerAngles.SetX(0);
            }
        }
    }
}