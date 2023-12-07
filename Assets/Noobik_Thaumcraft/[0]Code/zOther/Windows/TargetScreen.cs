using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public class TargetScreen : AnimatedScreen
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _labelInfo;

        [SerializeField]
        private TMP_Text _labelProgress;

        [SerializeField]
        private Slider _slider;

        private void OnDestroy()
        {
            
        }

        public void ViewUpgrade(int progress, TargetData data)
        {
            _icon.sprite = data.Icon;
            _labelInfo.text = $"Собери {data.Amount} кристаллов";
            _labelProgress.text = $"{progress}/{data.Amount}";
            _slider.maxValue = data.Amount;
            _slider.value = progress;
        }

        private void OnTargetDataUpdated(int index)
        {
            
        }
    }
}