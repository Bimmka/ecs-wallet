using System;
using System.Collections.Generic;
using System.Text;
using Currencies;
using UnityEngine;

namespace Wallet
{
    public class PlayerWallet
    {
        private readonly Dictionary<CurrencyType, int> _currencies = new Dictionary<CurrencyType, int>();

        public IReadOnlyCollection<KeyValuePair<CurrencyType, int>> Currencies => _currencies;
            
        public event Action<CurrencyType> Changed;
        
        public void InitializeDefaultValues()
        {
            _currencies.Clear();

            string[] names = Enum.GetNames(typeof(CurrencyType));

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] != CurrencyType.None.ToString())
                {
                    CurrencyType currencyType = Enum.Parse<CurrencyType>(names[i]); 
                    Add(currencyType, 0);
                }
            }
        }

        public void Restore(string saveData)
        {
            if (string.IsNullOrEmpty(saveData))
            {
                InitializeDefaultValues();
                return;
            }

            string[] currencies = saveData.Split("/");
            for (int i = 0; i < currencies.Length; i++)
            {
                if (string.IsNullOrEmpty(currencies[i]))
                    continue;

                string[] currencyElements = currencies[i].Split(":");

                if (currencyElements.Length < 2)
                    continue;

                if (Enum.TryParse(currencyElements[0], out CurrencyType currencyType) && int.TryParse(currencyElements[1], out int amount))
                {
                    Add(currencyType, amount);
                }
            }
            
            if (_currencies.Count == 0)
                InitializeDefaultValues();
        }

        public string Serialize()
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<CurrencyType,int> currency in Currencies)
            {
                builder.Append($"{currency.Key.ToString()}:{currency.Value}/");
            }
            return builder.ToString();
        }

        public void Add(CurrencyType currencyType, int amount)
        {
            _currencies.TryAdd(currencyType, 0);
            _currencies[currencyType] += amount;
            NotifyAboutChange(currencyType);
        }

        public void Decrease(CurrencyType currencyType, int amount)
        {
            if (_currencies.ContainsKey(currencyType))
            {
                _currencies[currencyType] -= amount;
                NotifyAboutChange(currencyType);
            }
            else
                Debug.LogWarning($"Try remove currency {currencyType} that's not in wallet");
        }

        public int GetAmount(CurrencyType currencyType)
        {
            if (_currencies.TryGetValue(currencyType, out int amount))
                return amount;

            return 0;
        }

        public bool IsEnough(CurrencyType currencyType, int amount)
        {
            return GetAmount(currencyType) - amount >= 0;
        }

        private void NotifyAboutChange(CurrencyType currencyType)
        {
            Changed?.Invoke(currencyType);
        }
    }
}