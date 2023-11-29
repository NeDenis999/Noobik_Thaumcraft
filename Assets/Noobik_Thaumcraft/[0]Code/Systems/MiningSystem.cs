using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningSystem : IEcsRunSystem
    {
        private EcsFilter<HeroComponent, HasTargetBlockComponent, RotateComponent>.Exclude<MiningDurationComponent, MoveEvent> _TryMiningFilter;
        private EcsFilter<HeroComponent, HasTargetBlockComponent, MiningDurationComponent> _miningFilter;
        private EcsFilter<MiningDurationComponent, MoveEvent> _stopMiningFilter;
        
        private SaveLoadService _saveLoad;
        
        public void Run()
        {
            foreach (var i in _TryMiningFilter)
            {
                ref var entity = ref _TryMiningFilter.GetEntity(i);
                entity.Get<MiningDurationComponent>().Timer = 1f;
                
                ref var rotateComponent = ref _TryMiningFilter.Get3(i);
                ref var targetComponent = ref _TryMiningFilter.Get2(i);
                
                ref var transform = ref rotateComponent.Transform;
                ref var rotation = ref rotateComponent.Quaternion;
                ref var target = ref targetComponent.Behaviour;

                var direction = Vector3.Normalize(target.Entity.Get<BlockComponent>().GameObject.transform.position -
                                                  transform.position);
                
                Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);
                toRotation.eulerAngles = toRotation.eulerAngles.SetX(0).SetZ(0);
                rotation = toRotation;
            }
            
            foreach (var i in _miningFilter)
            {
                ref var heroEntity = ref _miningFilter.GetEntity(i);
                ref var blockEntity = ref _miningFilter.Get2(i).Behaviour.Entity;
                ref var timer = ref _miningFilter.Get3(i);

                if (timer.Timer > 0)
                {
                    timer.Timer -= Time.deltaTime;
                    continue;
                }
                
                ref var blockComponent = ref blockEntity.Get<BlockComponent>();
                blockComponent.View.PlayBreak();
                blockComponent.Rigidbody.isKinematic = false;
                blockComponent.Rigidbody.useGravity = true;
                blockComponent.Rigidbody.AddForce(new Vector3(Random.Range(-2f, 2f), 3, Random.Range(-2f, 2f)), ForceMode.Impulse);
                blockComponent.GameObject.layer = 6;
                blockComponent.GameObject.tag = "PickItem";
                blockEntity.Get<BreakTag>();
                blockEntity.Get<PickUpDuration>().Timer = 0.5f;

                blockEntity.Get<PickItemComponent>().Rigidbody = blockComponent.Rigidbody;
                blockEntity.Get<PickItemComponent>().Collider = blockComponent.GameObject.GetComponent<Collider>();
                blockEntity.Get<PickItemComponent>().Type = blockComponent.ResourceType;
                blockEntity.Get<PickItemComponent>().GameObject = blockComponent.GameObject;

                heroEntity.Del<HasTargetBlockComponent>();
                
                ref var entity = ref _miningFilter.GetEntity(i);
                entity.Del<MiningDurationComponent>();
            }
            
            foreach (var i in _stopMiningFilter)
            {
                ref var entity = ref _stopMiningFilter.GetEntity(i);
                entity.Del<MiningDurationComponent>();
            }
        }
    }
}