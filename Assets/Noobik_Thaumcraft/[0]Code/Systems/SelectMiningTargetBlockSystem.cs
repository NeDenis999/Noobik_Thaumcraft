using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class SelectMiningTargetBlockSystem : IEcsRunSystem
    {
        private EcsFilter<HeroComponent, TransformComponent> _heroFilter;
        private EcsFilter<TriggerBlockComponent> _blockTriggerFilter;

        public void Run()
        {
            foreach (var heroIndex in _heroFilter)
            {
                var heroPosition = _heroFilter.Get2(heroIndex).Transform.position;

                var minDistance = float.MaxValue;
                EntityBehaviour nearestBlock = null;
                
                foreach (var blockTriggerIndex in _blockTriggerFilter)
                {
                    ref var blockBehaviour = ref _blockTriggerFilter.Get1(blockTriggerIndex).Block;
                 
                    if (!blockBehaviour.Entity.Has<CanMining>())
                        continue;

                    var distance = Vector3.Distance(blockBehaviour.transform.position, heroPosition);
                    
                    if (distance < minDistance)
                    {
                        nearestBlock = blockBehaviour;
                        minDistance = distance;
                    }
                    
                    _heroFilter.GetEntity(heroIndex).Get<HasTargetBlockComponent>().Behaviour = nearestBlock;
                }
                
                if (!nearestBlock)
                    _heroFilter.GetEntity(heroIndex).Del<HasTargetBlockComponent>();
            }
        }
    }
}