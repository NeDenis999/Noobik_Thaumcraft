using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private SerializablePair<WindowType, BaseWindow>[] _windows;

        public void Show(WindowType type, object args = null, bool hideOther = true)
        {
            if (hideOther)
                HideAll();

            foreach (var pair in _windows)
            {
                if (pair.Key == type)
                {
                    pair.Value.Show();
                    pair.Value.Bind(args);
                }
            }
        }

        public void HideAll()
        {
            foreach (var pair in _windows)
                pair.Value.Hide();
        }
    }
}