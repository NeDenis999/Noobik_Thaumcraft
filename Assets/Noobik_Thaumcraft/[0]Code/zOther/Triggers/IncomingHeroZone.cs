using Leopotam.Ecs;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class IncomingHeroZone : MonoBehaviour
    {
        [SerializeField]
        private EntityReference _machine;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
                return;

            ref var hero = ref _machine.Entity.Get<IncomingHeroTriggerComponent>();
            hero.Reference = other.GetComponent<EntityReference>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player")
                return;
            
            _machine.Entity.Del<IncomingHeroTriggerComponent>();
        }
    }
}