using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MiningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeroTag, TargetBlockComponent, MiningDurationComponent> _heroFilter;
        private readonly SaveLoadService _saveLoad;
        
        public void Run()
        {
            foreach (var i in _heroFilter)
            {
                ref var heroEntity = ref _heroFilter.GetEntity(i);
                ref var blockEntity = ref _heroFilter.Get2(i).Reference.Entity;
                ref var timer = ref _heroFilter.Get3(i);

                if (timer.Timer > 0)
                {
                    timer.Timer -= Time.deltaTime;
                    continue;
                }
                
                //
                
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

                heroEntity.Del<TargetBlockComponent>();
                
                ref var entity = ref _heroFilter.GetEntity(i);
                entity.Del<MiningDurationComponent>();
            }
        }
    }
}