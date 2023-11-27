using Leopotam.Ecs;

namespace Noobik_Thaumcraft
{
    public class StopMiningToMoveSystem : IEcsRunSystem
    {
        protected EcsFilter<MiningDurationComponent, MoveEvent> _heroFilter = null;

        public void Run()
        {
            foreach (var i in _heroFilter)
            {
                ref var entity = ref _heroFilter.GetEntity(i);
                entity.Del<MiningDurationComponent>();
            }
        }
    }
}