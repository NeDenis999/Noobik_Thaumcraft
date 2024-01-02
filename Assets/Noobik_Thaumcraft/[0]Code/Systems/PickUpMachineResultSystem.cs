using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickUpMachineResultSystem : IEcsRunSystem
    {
        private EcsFilter<TimerNotCanResultComponent> _notMoveFilter;
        private EcsFilter<EventResultTriggeredMachine> _triggerFilter;
        private EcsFilter<MachineComponent> _machineFilter;
        private EcsFilter<HeroComponent, BackpackItemsComponent> _heroFilter;

        private EcsWorld _world;
        private SceneData _sceneData;
        private Configuration _config;
        private GameData _data;

        public void Run()
        {
            foreach (var index in _notMoveFilter)
            {
                ref var timer = ref _notMoveFilter.Get1(index);

                if (timer.Timer > 0)
                    timer.Timer -= Time.deltaTime;
                else
                    _notMoveFilter.GetEntity(index).Del<TimerNotCanResultComponent>();
            }
            
            foreach (var triggerIndex in _triggerFilter)
            {
                foreach (var heroIndex in _heroFilter)
                {
                    var heroEntity = _heroFilter.GetEntity(heroIndex);
                    ref var machine = ref _triggerFilter.Get1(triggerIndex).Machine.Entity.Get<MachineComponent>();
                    ref var backpackComponent = ref _heroFilter.Get2(0);

                    if (backpackComponent.Items.Count >= _data.MaxItems)
                        continue;
                    
                    if (machine.ResultItems.Count == 0)
                        continue;
                    
                    //создание анимации движенияn
                    var item =  machine.ResultItems[machine.ResultItems.Count - 1];
                    ref var eventComponent = ref _world.NewEntity().Get<EventItemStartMove>();
                    eventComponent.Item = item;
                    eventComponent.Target = _heroFilter.Get1(heroIndex).CharacterController.GetComponent<EntityBehaviour>();
                    
                    machine.ResultItems.Remove(item);
                    heroEntity.Get<TimerNotCanResultComponent>().Timer = _config.NotDropTime;
                }
            }
        }
    }
}