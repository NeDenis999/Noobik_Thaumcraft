using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningSystem : IEcsRunSystem
    {
        private EcsFilter<HeroComponent, HasTargetBlockComponent, RotateComponent>.Exclude<TimerMiningComponent, EventMove> _heroTryMiningFilter;
        private EcsFilter<HeroComponent, HasTargetBlockComponent, TimerMiningComponent> _heroCanMiningBlockFilter;
        private EcsFilter<TimerMiningComponent, EventMove, HeroComponent> _stopMiningFilter;

        private SaveLoadService _saveLoad;
        private GameData _data;
        
        public void Run()
        {
            foreach (var i in _heroTryMiningFilter)
            {
                ref var entity = ref _heroTryMiningFilter.GetEntity(i);
                ref var hero = ref _heroTryMiningFilter.Get1(i);
                entity.Get<TimerMiningComponent>().Timer = 1f - 0.2f * _data.MiningBoost;
                
                ref var rotateComponent = ref _heroTryMiningFilter.Get3(i);
                ref var targetComponent = ref _heroTryMiningFilter.Get2(i);
                
                ref var transform = ref rotateComponent.Transform;
                ref var rotation = ref rotateComponent.Quaternion;
                ref var target = ref targetComponent.Behaviour;

                var direction = Vector3.Normalize(target.Entity.Get<BlockComponent>().GameObject.transform.position -
                                                  transform.position);
                
                Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);
                toRotation.eulerAngles = toRotation.eulerAngles.SetX(0).SetZ(0);
                rotation = toRotation;

                hero.Animator.PlayBreak();
                hero.ToolContainer.SetActive(true);
            }
            
            foreach (var i in _heroCanMiningBlockFilter)
            {
                ref var heroEntity = ref _heroCanMiningBlockFilter.GetEntity(i);
                ref var blockEntity = ref _heroCanMiningBlockFilter.Get2(i).Behaviour.Entity;
                ref var timer = ref _heroCanMiningBlockFilter.Get3(i);

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
                
                blockEntity.Get<CanPickUpComponent>();
                blockEntity.Get<TimerNotPickUpBlockComponent>().Timer = 0.5f;
                blockEntity.Get<PickItemComponent>().Rigidbody = blockComponent.Rigidbody;
                blockEntity.Get<PickItemComponent>().Collider = blockComponent.GameObject.GetComponent<Collider>();
                blockEntity.Get<PickItemComponent>().Type = blockComponent.ResourceType;
                blockEntity.Get<PickItemComponent>().GameObject = blockComponent.GameObject;
                blockEntity.Del<CanMining>();
                
                ((BoxCollider)blockEntity.Get<PickItemComponent>().Collider).size = Vector3.one * 0.5f; 
                blockComponent.GameObject.transform.localScale = Vector3.one;
                
                heroEntity.Del<HasTargetBlockComponent>();
                heroEntity.Del<TimerMiningComponent>();
                
                ref var hero = ref _stopMiningFilter.Get3(i);
                
                hero.Animator.StopBreak();
                hero.ToolContainer.SetActive(false);
            }
            
            foreach (var i in _stopMiningFilter)
            {
                ref var entity = ref _stopMiningFilter.GetEntity(i);
                entity.Del<TimerMiningComponent>();

                ref var hero = ref _stopMiningFilter.Get3(i);
                
                hero.Animator.StopBreak();
                hero.ToolContainer.SetActive(false);
            }
        }
    }
}