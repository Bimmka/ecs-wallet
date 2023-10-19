using System.Collections.Generic;
using Currencies;
using ECS.Components.Transactions;
using Transactions;
using UnityEngine;
using UnityEngine.UI;
using Wallet;
using Zenject;

namespace UI.Elements
{
    public class WalletDisplayer : MonoBehaviour
    {
        [SerializeField] private CurrencyElementView _currencyElementPrefab;
        [SerializeField] private RectTransform _spawnParent;
        [SerializeField] private Button _resetButton;

        private readonly Dictionary<CurrencyType, CurrencyElementView> _spawnedElementsByCurrency = new Dictionary<CurrencyType, CurrencyElementView>();
        private PlayerWallet _wallet;
        private TransactionsHandler _transactionsHandler;

        [Inject]
        private void Construct(PlayerWallet wallet, TransactionsHandler transactionsHandler)
        {
            _transactionsHandler = transactionsHandler;
            _wallet = wallet;
            _wallet.Changed += OnCurrencyChange;
        }

        private void Awake()
        {
            DisplayCurrenciesInWallet();
            _resetButton.onClick.AddListener(OnCurrenciesResetClick);
        }

        private void OnDestroy()
        {
            _wallet.Changed -= OnCurrencyChange;
            _resetButton.onClick.RemoveListener(OnCurrenciesResetClick);
        }
        
        private void DisplayCurrenciesInWallet()
        {
            foreach (KeyValuePair<CurrencyType,CurrencyElementView> elementView in _spawnedElementsByCurrency)
            {
                elementView.Value.CleanUp();
                elementView.Value.gameObject.SetActive(false);
            }
            
            foreach (KeyValuePair<CurrencyType,int> currency in _wallet.Currencies)
            {
                CurrencyElementView elementView;
                if (_spawnedElementsByCurrency.TryGetValue(currency.Key, out elementView) == false)
                {
                    elementView = Instantiate(_currencyElementPrefab, _spawnParent);
                    _spawnedElementsByCurrency.Add(currency.Key, elementView);
                }
                
                elementView.Initialize(currency.Key, OnCurrencyAddClick, OnCurrencyDecreaseClick);
                elementView.Display(currency.Key.ToString(), currency.Value);
            }
        }

        private void OnCurrencyChange(CurrencyType currencyType)
        {
            CurrencyElementView elementView;
            if (_spawnedElementsByCurrency.TryGetValue(currencyType, out elementView) == false)
            {
                elementView = Instantiate(_currencyElementPrefab, _spawnParent);
                _spawnedElementsByCurrency.Add(currencyType, elementView);
                elementView.Initialize(currencyType, OnCurrencyAddClick, OnCurrencyDecreaseClick);
            }
            
            elementView.Display(currencyType.ToString(), _wallet.GetAmount(currencyType));
        }

        private void OnCurrencyAddClick(CurrencyType currencyType)
        {
            _transactionsHandler.AddTransaction(TransactionType.Add, currencyType, 1);
        }

        private void OnCurrencyDecreaseClick(CurrencyType currencyType)
        {
            _transactionsHandler.AddTransaction(TransactionType.Decrease, currencyType, 1);
        }

        private void OnCurrenciesResetClick()
        {
            _transactionsHandler.AddTransaction(TransactionType.Reset, CurrencyType.None, 0);
        }
    }
}