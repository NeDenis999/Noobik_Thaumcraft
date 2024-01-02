using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class OpenSkillShopTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Startup.Instantiate.UI.SkillShopScreen.Show(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                Startup.Instantiate.UI.SkillShopScreen.Show(false);
            }
        }
    }
}