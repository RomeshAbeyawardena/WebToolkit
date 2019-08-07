using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using WebToolkit.Common.Attributes;
using WebToolkit.Common.Builders;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Builders;

namespace WebToolkit.Common
{
    public sealed class Switch
    {
        public static DefaultSwitch Default => new DefaultSwitch();

        public static ISwitch<TKey, TValue> Create<TKey, TValue>(
            Func<IDictionaryBuilder<TKey, TValue>, IDictionaryBuilder<TKey, TValue>> switchDictionaryBuilder,
            Func<object> defaultValueExpression = null)
        {
            return Switch<TKey, TValue>.Create(switchDictionaryBuilder, defaultValueExpression);
        }

        public static ISwitch<TKey, TValue> Create<TKey, TValue>(IDictionary<TKey, TValue> switchDictionary = null,
            Func<object> defaultValueExpression = null)
        {
            return Switch<TKey, TValue>.Create(switchDictionary, defaultValueExpression);
        }
    }

    public sealed class Switch<TKey, TValue> : ISwitch<TKey, TValue>
    {
        public static ISwitch<TKey, TValue> Create(Func<IDictionaryBuilder<TKey, TValue>, IDictionaryBuilder<TKey, TValue>> switchDictionaryBuilder, 
            Func<object> defaultValueExpression = null)
        {
            return Create(switchDictionaryBuilder(DictionaryBuilder<TKey, TValue>.Create())
                .ToDictionary(), defaultValueExpression);
        }

        public static ISwitch<TKey, TValue> Create(IDictionary<TKey, TValue> switchDictionary = null, 
            Func<object> defaultValueExpression = null)
        {
            if (switchDictionary == null)
                switchDictionary = new Dictionary<TKey, TValue>();

            return new Switch<TKey, TValue>(switchDictionary, defaultValueExpression);
        }

        public ISwitch<TKey, TValue> CaseWhen(TKey key, TValue value)
        {
            if (_switchDictionary.ContainsKey(key))
                throw new ArgumentException($"Switch already contains a value for {key}", nameof(key));

            _switchDictionary.Add(key, value);
            return this;
        }

        public ISwitch<TKey, TValue> CaseWhen(TKey key, Func<TValue> valueExpression)
        {
            if (valueExpression == null)
                throw new ArgumentNullException(nameof(valueExpression));

            return CaseWhen(key, valueExpression());
        }

        public ISwitch<TKey, TValue> CaseWhenDefault(TValue value)
        {
            return CaseWhenDefault(() => value);
        }

        public ISwitch<TKey, TValue> CaseWhenDefault(Func<object> valueExpression)
        {
            _defaultValueExpression = valueExpression;
            return this;
        }

        public TValue Case(TKey key)
        {
            if (_switchDictionary.TryGetValue(key, out var value))
                return value;

            
            if (_defaultValueExpression == null)
                throw new ArgumentException($"Unable to find a value for {key}", nameof(key));

            var defaultValue = _defaultValueExpression();

            if (defaultValue is DefaultSwitch)
                return default;

            return (TValue)defaultValue;

        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            return _switchDictionary;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _switchDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        private Switch(IDictionary<TKey, TValue> switchDictionary, Func<object> defaultValueExpression)
        {
            var defaultSwitchCaseAttribute = (DefaultSwitchCaseAttribute)GetType().GetCustomAttribute(typeof(DefaultSwitchCaseAttribute));

            if (defaultSwitchCaseAttribute != null)
                defaultValueExpression = defaultSwitchCaseAttribute.Value;

            _switchDictionary = switchDictionary;
            _defaultValueExpression = defaultValueExpression;
        }

        private Func<object> _defaultValueExpression;
        private readonly IDictionary<TKey, TValue> _switchDictionary;
    }

    public sealed class DefaultSwitch
    {

    }
}