using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class NotDropTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NotDropDurationComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var timer = ref _filter.Get1(index);
                timer.Timer -= Time.deltaTime;
                
                if (timer.Timer <= 0)
                    entity.Del<NotDropDurationComponent>();
            }
        }
    }
}