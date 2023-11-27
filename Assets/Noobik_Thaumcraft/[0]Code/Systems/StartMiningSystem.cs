using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class StartMiningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeroTag, TargetBlockComponent, RotateComponent>
            .Exclude<MiningDurationComponent, MoveEvent> _heroFilter = null;

        public void Run()
        {
            foreach (var i in _heroFilter)
            {
                ref var entity = ref _heroFilter.GetEntity(i);
                entity.Get<MiningDurationComponent>().Timer = 1f;
                
                ref var rotateComponent = ref _heroFilter.Get3(i);
                ref var targetComponent = ref _heroFilter.Get2(i);
                
                ref var transform = ref rotateComponent.Transform;
                ref var rotation = ref rotateComponent.Quaternion;
                ref var target = ref targetComponent.Reference;

                var direction = Vector3.Normalize(target.Entity.Get<BlockComponent>().GameObject.transform.position -
                 transform.position);
                
                Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);
                toRotation.eulerAngles = toRotation.eulerAngles.SetX(0).SetZ(0);
                rotation = toRotation;
            }
        }
    }
}