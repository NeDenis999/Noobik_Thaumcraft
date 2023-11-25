using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class BaseWindow : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        public void Bind(object args)
        {
            if (args == null)
                return;

            OnBind(args);
        }

        protected virtual void OnBind(object args)
        {
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}