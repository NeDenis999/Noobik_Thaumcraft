using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class BlockAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private static readonly int BreakHash = Animator.StringToHash("Break");
        private static readonly int StayHash = Animator.StringToHash("Stay");

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