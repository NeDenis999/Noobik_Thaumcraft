using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class Screen : MonoBehaviour
    {
        public virtual void Show(bool state)
        {
            if (state)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}