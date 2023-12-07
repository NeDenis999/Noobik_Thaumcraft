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

        public void PlayBreak()
        {
            _animator.SetTrigger("Break");
        }

        public void StopBreak()
        {
            _animator.SetTrigger("StopBreak");
        }
    }
}