using System;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public sealed class Lock<TResult> : ILock<TResult>
    {
        private readonly object _lockObject = new object();
        public TResult Value { get; private set; }
        public Func<TResult, TResult> DoWork { get; }
        public void Run()
        {
            lock (_lockObject)
            {
                Value = DoWork(Value);
            }
        }

        public Lock(TResult value, Func<TResult, TResult> doWork)
        {
            Value = value;
            DoWork = doWork;
        }
    }
}