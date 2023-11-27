using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class RotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotateComponent> _rotateFilter = null;

        public void Run()
        {
            foreach (var i in _rotateFilter)
            {
                ref var rotateComponent = ref _rotateFilter.Get1(i);
                
                ref var transform = ref rotateComponent.Transform;
                ref var speed = ref rotateComponent.Speed;
                ref var rotation = ref rotateComponent.Quaternion;
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed * Time.deltaTime);  
            }
        }
    }
}