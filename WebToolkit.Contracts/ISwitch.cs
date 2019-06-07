using System;
using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface ISwitch<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        ISwitch<TKey, TValue> CaseWhen(TKey key, TValue value);
        ISwitch<TKey, TValue> CaseWhen(TKey key, Func<TValue> valueExpression);
        ISwitch<TKey, TValue> CaseWhenDefault(TValue value);
        ISwitch<TKey, TValue> CaseWhenDefault(Func<TValue> valueExpression);
        TValue Case(TKey key);

        IDictionary<TKey, TValue> ToDictionary();
    }
}