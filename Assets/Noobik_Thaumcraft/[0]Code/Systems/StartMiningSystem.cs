using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class StartMiningSystem : IEcsRunSystem
    {
        protected EcsFilter<HeroTag, BlocksComponent>.Exclude<MiningDurationComponent> _heroFilter = null;

        public void Run()
        {
            foreach (var i in _heroFilter)
            {
                ref var entity = ref _heroFilter.GetEntity(i);
                entity.Get<MiningDurationComponent>().Timer = 1f;
            }
        }
    }
}