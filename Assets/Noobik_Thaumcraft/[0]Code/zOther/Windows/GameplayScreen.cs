using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public class GameplayScreen : Screen
    {
        [SerializeField]
        private Button _shopButton;

        [SerializeField]
        private Button _soundButton;
        
        [SerializeField]
        private Button _musicButton;

        [SerializeField]
        private Button _mapButton;
        
        [SerializeField]
        private Image _soundImage;
        
        [SerializeField]
        private Image _musicImage;

        [SerializeField]
        private TMP_Text _diamondLabel;
        
        [Header("Services")]
        [SerializeField]
        private UI _ui;

        [SerializeField]
        private SaveLoadService _saveLoadService;
        
        [Header("Settings")]
        [SerializeField]
        private Sprite _soundOn;
        
        [SerializeField]
        private Sprite _soundOff;
        
        [SerializeField]
        private Sprite _musicOn;
        
        [SerializeField]
        private Sprite _musicOff;
        
        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnClickedShopButton);
            _soundButton.onClick.AddListener(OnClickedSoundButton);
            _musicButton.onClick.AddListener(OnClickedMusicButton);
            _mapButton.onClick.AddListener(OnClickedMapButton);

            _saveLoadService.DiamondStorage.Upgrade += OnDiamondLabelUpgrade;
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnClickedShopButton);
            _soundButton.onClick.RemoveListener(OnClickedSoundButton);
            _musicButton.onClick.RemoveListener(OnClickedMusicButton);
            _mapButton.onClick.RemoveListener(OnClickedMapButton);
            
            _saveLoadService.DiamondStorage.Upgrade -= OnDiamondLabelUpgrade;
        }

        private void Start()
        {
            OnDiamondLabelUpgrade(_saveLoadService.DiamondStorage.Get());
            _soundImage.sprite = _saveLoadService.SoundStorage.Get() ? _soundOn : _soundOff;
            _musicImage.sprite = _saveLoadService.MusicStorage.Get() ? _musicOn : _musicOff;
        }

        private void OnClickedShopButton()
        {
            _ui.ShopScreen.Show(true);
        }

        private void OnClickedSoundButton()
        {
            var isActive = !_saveLoadService.SoundStorage.Get();
            _soundImage.sprite = isActive ? _soundOn : _soundOff;
            _saveLoadService.SoundStorage.Set(isActive);
        }

        private void OnClickedMusicButton()
        {
            var isActive = !_saveLoadService.MusicStorage.Get();
            _musicImage.sprite = isActive ? _musicOn : _musicOff;
            _saveLoadService.MusicStorage.Set(isActive);
        }

        private void OnClickedMapButton()
        {
            _ui.MapScreen.Show(true);
        }

        private void OnDiamondLabelUpgrade(int value)
        {
            _diamondLabel.text = value.ToString();
        }

    }
}