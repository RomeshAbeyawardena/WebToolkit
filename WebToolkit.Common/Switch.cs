using System;
using System.Collections;
using System.Collections.Generic;
using WebToolkit.Common.Builders;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Builders;

namespace WebToolkit.Common
{
    public class Switch<TKey, TValue> : ISwitch<TKey, TValue>
    {
        public static Switch<TKey, TValue> Create(IDictionaryBuilder<TKey, TValue> switchDictionary = null)
        {
            if(switchDictionary == null)
                switchDictionary = DictionaryBuilder<TKey, TValue>.Create();

            return new Switch<TKey, TValue>(switchDictionary.ToDictionary());
        }

        public static Switch<TKey, TValue> Create(IDictionary<TKey, TValue> switchDictionary = null)
        {
            if(switchDictionary == null)
                switchDictionary = new Dictionary<TKey, TValue>();

            return new Switch<TKey, TValue>(switchDictionary);
        }

        public ISwitch<TKey, TValue> CaseWhen(TKey key, TValue value)
        {
            if (_switchDictionary.ContainsKey(key))
                throw  new ArgumentException($"Switch already contains a value for {key}", nameof(key));

            _switchDictionary.Add(key, value);
                return this;
        }

        public ISwitch<TKey, TValue> CaseWhen(TKey key, Func<TValue> valueExpression)
        {
            if(valueExpression == null)
                throw new ArgumentNullException(nameof(valueExpression));

            return CaseWhen(key, valueExpression());
        }

        public TValue Case(TKey key)
        {
            if (_switchDictionary.TryGetValue(key, out var value))
                return value;

            throw new ArgumentException($"Unable to find a value for {key}", nameof(key));
        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            return _switchDictionary;
        }

        private Switch(IDictionary<TKey, TValue> switchDictionary)
        {
            _switchDictionary = switchDictionary;
        }

        private readonly IDictionary<TKey, TValue> _switchDictionary;
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _switchDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}