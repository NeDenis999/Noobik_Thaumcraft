using UnityEngine;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public class MapWindow : BaseWindow
    {
        [SerializeField]
        private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClickedCloseButton);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClickedCloseButton);
        }

        private void OnClickedCloseButton()
        {
            Hide();
        }
    }
}