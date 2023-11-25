using System;
using UnityEngine;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private Button _shopButton;
        
        [SerializeField]
        private Button _shinButton;

        [SerializeField]
        private GameObject _shop;

        [SerializeField]
        private GameObject _skin;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClickedCloseButton);
            _shopButton.onClick.AddListener(OnClickedShopButton);
            _shinButton.onClick.AddListener(OnClickedSkinButton);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClickedCloseButton);
            _shopButton.onClick.RemoveListener(OnClickedShopButton);
            _shinButton.onClick.RemoveListener(OnClickedSkinButton);
        }

        private void Start()
        {
            OnClickedShopButton();
        }

        private void OnClickedCloseButton()
        {
            Hide();
        }

        private void OnClickedShopButton()
        {
            _shopButton.interactable = false;
            _shinButton.interactable = true;
            
            _shop.SetActive(true);
            _skin.SetActive(false);
        }

        private void OnClickedSkinButton()
        {
            _shopButton.interactable = true;
            _shinButton.interactable = false;
            
            _shop.SetActive(false);
            _skin.SetActive(true);
        }
    }
}