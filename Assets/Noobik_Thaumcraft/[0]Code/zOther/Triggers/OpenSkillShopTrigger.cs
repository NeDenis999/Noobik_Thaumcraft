using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class OpenSkillShopTrigger : MonoBehaviour
    {
        [SerializeField] 
        private UI _ui;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                _ui.SkillShopScreen.Show(true);
            }
        }
    }
}