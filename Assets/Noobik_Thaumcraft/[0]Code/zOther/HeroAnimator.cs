using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class HeroAnimator : MonoBehaviour
    {

        [SerializeField]
        private Animator _animator;
        
        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
}