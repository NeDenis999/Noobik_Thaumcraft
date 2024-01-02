using TMPro;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class CountItemsLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private int _count;
        private int _size;

        public void ViewUpdate(int count, int size)
        {
            _count = count;
            _size = size;

            ViewUpdate();
        }

        public void SetSize(int size)
        {
            _size = size;
            ViewUpdate();
        }
        
        private void ViewUpdate()
        {
            _label.text = $"{_count}/{_size}";
        }
    }
}