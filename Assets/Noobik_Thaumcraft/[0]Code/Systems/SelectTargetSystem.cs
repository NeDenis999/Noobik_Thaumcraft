using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class SelectTargetSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        private GameData _gameData;
        private Configuration _configuration;
        
        private EcsFilter<HeroComponent, TransformComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var transform = _filter.Get2(i).Transform;
                var target = GetTargetPoint(transform);
                _gameData.TargetPointStorage.Set(target);
            }
        }
        
        private Transform GetTargetPoint(Transform heroTransform)
        {
            foreach (var point in _sceneData.TargetPoints)
            {
                if (point.Key == _configuration.TargetData[_gameData.TargetDataIndexStorage.Get()].Location)
                {
                    float minDistance = float.MaxValue;
                    Transform result = point.Value[0];
                    
                    foreach (var target in point.Value)
                    {
                        var distance = Vector3.Distance(target.position, heroTransform.position);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            result = target;
                        }
                    }
                    
                    return result;
                }
            }

            return null;
        }
    }
}