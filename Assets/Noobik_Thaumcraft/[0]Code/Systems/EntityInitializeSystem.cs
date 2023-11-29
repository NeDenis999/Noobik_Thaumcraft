using Leopotam.Ecs;

namespace Noobik_Thaumcraft
{
    public class EntityInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> _initFilter;

        public void Run()
        {
            foreach (var i in _initFilter)
            {
                ref var entity = ref _initFilter.GetEntity(i);
                ref var request = ref _initFilter.Get1(i);
                request.entityBehaviour.Entity = entity;
                
                entity.Del<InitializeEntityRequest>();
            }
        }
    }
}