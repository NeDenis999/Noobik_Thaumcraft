using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TargetBlockSelectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeroComponent, TransformComponent, BlocksComponent> _heroFilter;
        private readonly SaveLoadService _saveLoad;
        
        public void Run()
        {
            foreach (var index in _heroFilter)
            {
                ref var blocks = ref _heroFilter.Get3(index);

                if (blocks.Entities == null)
                    continue;
                
                ref var transform = ref _heroFilter.Get2(index).Transform;

                float minDistance = float.MaxValue;
                EntityBehaviour result = null;
                
                foreach (var reference in blocks.Entities)
                {
                    if (reference.Entity.Has<BreakTag>())
                        continue;

                    ref var block = ref reference.Entity.Get<BlockComponent>();
                    var distance = Vector3.Distance(block.GameObject.transform.position, transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        result = reference;
                    }
                }

                if (result != null)
                    _heroFilter.GetEntity(index).Get<HasTargetBlockComponent>().Behaviour = result;
            }
        }
    }
}