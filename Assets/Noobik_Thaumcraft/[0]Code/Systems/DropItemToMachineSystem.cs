using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class DropItemToMachineSystem : IEcsRunSystem
    {
        protected EcsFilter<MachineComponent, IncomingHeroTriggerComponent> _machineFilter = null;

        public void Run()
        {
            foreach (var index in _machineFilter)
            {
                ref var entity = ref _machineFilter.GetEntity(index);
                ref var heroEntity = ref entity.Get<IncomingHeroTriggerComponent>().Reference.Entity;

                ref var items = ref heroEntity.Get<BackpackItemsComponent>();
                
                if (items.References == null || items.References.Count == 0)
                    continue;
                
                var item = items.References[^1];
                items.References.Remove(item);

                if (heroEntity.Has<BlocksComponent>() && heroEntity.Get<BlocksComponent>().References.Contains(item))
                {
                    heroEntity.Get<BlocksComponent>().References.Remove(item);
                }
                
                Object.Destroy(item.Entity.Get<PickItemComponent>().GameObject);
                item.Entity.Destroy();
                
                if (items.References.Count == 0)
                    heroEntity.Del<BackpackItemsComponent>();
            }
        }
    }
}