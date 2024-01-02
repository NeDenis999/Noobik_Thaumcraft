using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Noobik_Thaumcraft
{
    public abstract class BaseShopCell : MonoBehaviour
    {
        [SerializeField] 
        private protected Button _buyButton;

        [SerializeField]
        private protected TMP_Text _levelLabel;
        
        [SerializeField]
        private protected TMP_Text _priceLabel;

        [SerializeField]
        private protected TMP_Text _buttonLabel;
        
        [SerializeField]
        private int _startPrice;

        [SerializeField] 
        private int _maxLevel;

        private int _level = 1;
        private int _price;
        
        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyClicked);
        }

        private void Start()
        {
            _price = _startPrice;

            ViewUpdate();
        }

        protected abstract void Buy();

        protected void OnBuyClicked()
        {
            Buy();

            _level += 1;
            _price += (int)(_startPrice * 0.5f);

            ViewUpdate();

            if (_level >= _maxLevel)
            {
                _buyButton.interactable = false;
                _buttonLabel.text = "Максимум";
                _priceLabel.text = "Максимум";
            }
        }

        private void ViewUpdate()
        {
            _levelLabel.text = $"Уровень {_level}";
            _priceLabel.text = $"{_price}";
        }
    }
}