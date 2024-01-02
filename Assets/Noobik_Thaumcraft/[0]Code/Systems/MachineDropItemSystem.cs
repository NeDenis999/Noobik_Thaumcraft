﻿using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MachineDropItemSystem : IEcsRunSystem
    {
        private EcsFilter<TimerNotCanDropComponent> _notDropFilter;
        private EcsFilter<HeroComponent, TransformComponent, BackpackItemsComponent>.Exclude<TimerNotCanDropComponent> _heroFilter;
        private EcsFilter<EventDropTriggeredMachine> _eventTriggeredFilter;
        private EcsFilter<ItemMoveToMachine> _itemMovedFilter;

        private EcsWorld _world;
        private GameData _data;
        private Configuration _config;

        public void Run()
        {
            foreach (var notDropIndex in _notDropFilter)
            {
                ref var timer = ref _notDropFilter.Get1(notDropIndex);

                if (timer.Timer > 0)
                    timer.Timer -= Time.deltaTime;
                else
                    _notDropFilter.GetEntity(notDropIndex).Del<TimerNotCanDropComponent>();
            }
            
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
                    eventComponent.Target = _eventTriggeredFilter.Get1(triggerIndex).Machine;
                    
                    //удаление предмета
                    items.Remove(item);
                    
                    //создание таймера
                    heroEntity.Get<TimerNotCanDropComponent>().Timer = _config.NotDropTime;
                }
            }
        }
    }
}