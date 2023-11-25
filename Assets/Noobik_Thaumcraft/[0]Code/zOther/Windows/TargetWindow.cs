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
        
        [SerializeField]
        private TargetsDataManager _targetsManager;

        private void OnEnable()
        {
            _targetsManager.TargetUpdated += OnTargetUpdated;
        }

        private void OnDisable()
        {
            _targetsManager.TargetUpdated -= OnTargetUpdated;
        }

        private void Start()
        {
            ViewUpgrade(_targetsManager.GetCurrentTarget());
        }

        private void ViewUpgrade(TargetData target)
        {
            _icon.sprite = target.Icon;
            _labelInfo.text = $"Собери {target.Amount} кристаллов";
            _labelProgress.text = $"{0}/{target.Amount}";
            _slider.value = 0;
        }

        private void OnTargetUpdated(TargetData target)
        {
            ViewUpgrade(target);
        }
    }
}