using Leopotam.Ecs;
using Noobik_Thaumcraft.Extensions;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class ArrowHideAtTargetDistanceSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private readonly EcsFilter<ArrowTargetTag, TransformComponent, HideAtDistanceComponent> _rotateFilter = null;

        public void Run()
        {
            foreach (var i in _rotateFilter)
            {
                ref var transform = ref _rotateFilter.Get2(i).Transform;
                ref var hide = ref _rotateFilter.Get3(i);
                
                var distance = (_gameData.TargetPointStorage.Get().position - transform.position).magnitude;
                var alpha = (Mathf.Clamp(distance, hide.MinDistance, hide.MaxDistance) - hide.MinDistance) / (hide.MaxDistance - hide.MinDistance);
                
                hide.Image.color = hide.Image.color.SetAlpha(alpha);
            }
        }
    }
}