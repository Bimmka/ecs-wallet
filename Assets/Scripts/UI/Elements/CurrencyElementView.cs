using System;
using Currencies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class CurrencyElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyNameText;
        [SerializeField] private TMP_Text _currencyAmountText;
        [SerializeField] private Button _addCurrencyButton;
        [SerializeField] private Button _decreaseCurrencyButton;

        private event Action<CurrencyType> onAddButtonClickSavedEvent;
        private event Action<CurrencyType> onDecreaseButtonClickSavedEvent;

        public CurrencyType CurrencyType { get; private set; }

        private void Awake()
        {
            _addCurrencyButton.onClick.AddListener(OnAddButtonClick);
            _decreaseCurrencyButton.onClick.AddListener(OnDecreaseButtonClick);
        }

        private void OnDestroy()
        {
            _addCurrencyButton.onClick.RemoveListener(OnAddButtonClick);
            _decreaseCurrencyButton.onClick.RemoveListener(OnDecreaseButtonClick);
        }

        public void Initialize(CurrencyType currencyType, Action<CurrencyType> onAddCurrencyClick, Action<CurrencyType> onDecreaseCurrencyClick)
        {
            CurrencyType = currencyType;
            onAddButtonClickSavedEvent = onAddCurrencyClick;
            onDecreaseButtonClickSavedEvent = onDecreaseCurrencyClick;
        }

        public void CleanUp()
        {
            onAddButtonClickSavedEvent = null;
            onDecreaseButtonClickSavedEvent = null;
        }

        public void Display(string currencyName, int amount)
        {
            _currencyNameText.text = currencyName;
            _currencyAmountText.text = amount.ToString();
        }

        private void OnAddButtonClick()
        {
            onAddButtonClickSavedEvent?.Invoke(CurrencyType);
        }

        private void OnDecreaseButtonClick()
        {
            onDecreaseButtonClickSavedEvent?.Invoke(CurrencyType);
        }
    }
}