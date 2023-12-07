using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MachineCreateSystem : IEcsRunSystem
    {
        private EcsFilter<MachineComponent, TimerMachineCreateComponent> _processingMachineFilter;
        private EcsFilter<MachineComponent>.Exclude<TimerMachineCreateComponent> _machineFilter;

        private EcsWorld _world;
        private Configuration _config;

        public void Run()
        {
            foreach (var machineIndex in _machineFilter)
            {
                if (_machineFilter.Get1(machineIndex).DropItems.Count > 0)
                {
                    ref var machineEntity = ref _machineFilter.GetEntity(machineIndex);
                    machineEntity.Get<TimerMachineCreateComponent>().Timer = 2f;
                }
            }
            
            foreach (var machineProcessingIndex in _processingMachineFilter)
            {
                ref var machine = ref _processingMachineFilter.Get1(machineProcessingIndex);
                ref var timer = ref _processingMachineFilter.Get2(machineProcessingIndex);

                if (timer.Timer > 0)
                {
                    machine.Slider.value = machine.Slider.maxValue * (2 - timer.Timer) / 2;
                    timer.Timer -= Time.deltaTime;
                }
                else
                {
                    _processingMachineFilter.GetEntity(machineProcessingIndex).Del<TimerMachineCreateComponent>();
                    machine.Slider.value = 0;
                    
                    var dropItems = machine.DropItems;
                    var dropItem = dropItems[dropItems.Count - 1];
                    Object.Destroy(dropItem.gameObject);
                    dropItems.Remove(dropItem);
                    
                    var result = Object.Instantiate(machine.ResultPrefab, machine.ResultContainer);
                    machine.ResultItems.Add(result);

                    result.transform.localScale = Vector3.one;
                    result.transform.localPosition = 
                        BlockHelper.GetBlockInContainerPosition(machine.ResultItems.Count, _config.DistanceCell);
                    //var resultEntity = _world.NewEntity();
                    //resultEntity.Get<>();
                }
            }
        }
    }
}