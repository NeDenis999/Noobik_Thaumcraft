using System;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public class TargetWindow : BaseWindow
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _labelInfo;

        [SerializeField]
        private TMP_Text _labelProgress;

        [SerializeField]
        private Slider _slider;

        private void Start()
        {
            Startup.Instantiate.GameData.TargetDataIndexStorage.Upgrade += OnTargetDataUpdated;
            
            ViewUpgrade(Startup.Instantiate.Configuration.TargetData[Startup.Instantiate.GameData
            .TargetDataIndexStorage.Get()]);
        }

        private void OnDestroy()
        {
            Startup.Instantiate.GameData.TargetDataIndexStorage.Upgrade -= OnTargetDataUpdated;
        }

        private void ViewUpgrade(TargetData target)
        {
            _icon.sprite = target.Icon;
            _labelInfo.text = $"Собери {target.Amount} кристаллов";
            _labelProgress.text = $"{0}/{target.Amount}";
            _slider.value = 0;
        }

        private void OnTargetDataUpdated(int index)
        {
            ViewUpgrade(Startup.Instantiate.Configuration.TargetData[index]);
        }
    }
}