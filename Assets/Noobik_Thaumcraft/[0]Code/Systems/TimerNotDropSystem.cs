using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TimerNotDropSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimerNotCanDropComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var timer = ref _filter.Get1(index);
                timer.Timer -= Time.deltaTime;
                
                if (timer.Timer <= 0)
                    entity.Del<TimerNotCanDropComponent>();
            }
        }
    }
}