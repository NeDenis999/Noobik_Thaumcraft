using Leopotam.Ecs;
using Noobik_Thaumcraft.Extensions;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class ArrowHideAtTargetDistanceSystem : IEcsRunSystem
    {
        private readonly TargetsDataManager _targetsManager = null;
        private readonly TargetPointsContainer _targetPointsContainer = null;
        
        private readonly EcsFilter<ArrowTargetTag, ModelComponent, HideAtDistanceComponent> _rotateFilter = null;

        public void Run()
        {
            foreach (var i in _rotateFilter)
            {
                ref var transform = ref _rotateFilter.Get2(i).ModelTransform;
                ref var hide = ref _rotateFilter.Get3(i);

                var targetPosition = _targetPointsContainer.GetPoint(_targetsManager.GetCurrentTarget().Location, transform)
                .position;
                var distance = (targetPosition - transform.position).magnitude;
                var alpha = (Mathf.Clamp(distance, hide.MinDistance, hide.MaxDistance) - hide.MinDistance) / (hide.MaxDistance - hide.MinDistance);
                
                hide.Image.color = hide.Image.color.SetAlpha(alpha);
            }
        }
    }
}