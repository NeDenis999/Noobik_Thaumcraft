using Leopotam.Ecs;

namespace Noobik_Thaumcraft
{
    public class TrashDropItemSystem : IEcsRunSystem
    {
        private EcsFilter<EventDropTriggeredTrashcan> _eventTriggeredFilter;
        private EcsFilter<HeroComponent, TransformComponent, BackpackItemsComponent>.Exclude<TimerNotCanDropComponent> _heroFilter;
        
        private EcsWorld _world;
        private Configuration _config;
        
        public void Run()
        {
            foreach (var triggerIndex in _eventTriggeredFilter)
            {
                foreach (var heroIndex in _heroFilter)
                {
                    var heroEntity = _heroFilter.GetEntity(heroIndex);
                    ref var backpackComponent = ref _heroFilter.Get3(0);
                    var items = backpackComponent.Items;
                
                    if (items.Count == 0)
                        continue;
                
                    var item = items[items.Count - 1];

                    //создание анимации движения
                    ref var eventComponent = ref _world.NewEntity().Get<EventItemStartMove>();
                    eventComponent.Item = item;
                    eventComponent.Target = _eventTriggeredFilter.Get1(triggerIndex).Trashcan;
                    
                    //удаление предмета
                    items.Remove(item);
                    
                    //создание таймера
                    heroEntity.Get<TimerNotCanDropComponent>().Timer = _config.NotDropTime;
                }
            }
        }
    }
}