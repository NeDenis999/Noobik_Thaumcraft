using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class PickItemAnimator : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;
        
        public void StopRotation()
        {
            _animator.SetTrigger("PickUp");   
        }
    }
}