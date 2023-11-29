using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class DropItemToMachineSystem : IEcsRunSystem
    {
        private EcsFilter<MachineComponent, IncomingHeroTriggerComponent>.Exclude<MoveEvent> _machineFilter;

        public void Run()
        {
            foreach (var index in _machineFilter)
            {
                ref var entity = ref _machineFilter.GetEntity(index);
                ref var heroEntity = ref entity.Get<IncomingHeroTriggerComponent>().Behaviour.Entity;

                if (heroEntity.Has<NotDropDurationComponent>())
                    continue;

                ref var items = ref heroEntity.Get<BackpackItemsComponent>();

                if (items.References == null || items.References.Count == 0)
                    continue;

                var item = items.References[^1];
                items.References.Remove(item);

                if (heroEntity.Has<BlocksComponent>() && heroEntity.Get<BlocksComponent>().Entities.Contains(item))
                {
                    heroEntity.Get<BlocksComponent>().Entities.Remove(item);
                }
                
                Object.Destroy(item.Entity.Get<PickItemComponent>().GameObject);
                item.Entity.Destroy();

                heroEntity.Get<NotDropDurationComponent>().Timer = 0.2f;
                
                if (items.References.Count == 0)
                    heroEntity.Del<BackpackItemsComponent>();
            }
        }
    }
}