using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class BlockAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private readonly static int BreakHash = Animator.StringToHash("Break");
        private readonly static int StayHash = Animator.StringToHash("Stay");

        public void PlayBreak()
        {
            _animator.SetTrigger(BreakHash);
        }

        public void PlayStay()
        {
            _animator.SetTrigger(StayHash);
        }
    }
}