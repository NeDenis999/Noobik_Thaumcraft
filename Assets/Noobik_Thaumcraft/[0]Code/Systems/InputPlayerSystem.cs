using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class InputPlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HeroComponent, DirectionComponent> _directionFilter = null;

        private float _moveX;
        private float _moveZ;
        
        public void Run()
        {
            SetDirection();
            
            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(i);
                directionComponent.Direction = new Vector3(_moveX, 0, _moveZ);

                if (IsMove())
                {
                    ref var entity = ref _directionFilter.GetEntity(i);
                    entity.Get<EventMove>();
                }
            }
        }

        private void SetDirection()
        {
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveZ = Input.GetAxisRaw("Vertical");
        }

        private bool IsMove()
        {
            return _moveX != 0 || _moveZ != 0;
        }
    }
}