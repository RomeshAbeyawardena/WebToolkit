using System;
using System.Threading;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public sealed class Lock<TResult> : ILock<TResult>
    {
        private readonly object _lockObject = new object();
        private readonly Random _random = new Random();
        public static Lock<TResult> Create(TResult initialValue, Func<TResult, TResult> doWork)
        {
            return new Lock<TResult>(initialValue, doWork);
        }

        public TResult Value { get; private set; }
        public Func<TResult, TResult> DoWork { get; }
        public void Run()
        {
            lock (_lockObject)
            {
                Value = DoWork(Value);
                Thread.Sleep(_random.Next(100, 1000));
            }
        }

        private Lock(TResult initialValue, Func<TResult, TResult> doWork)
        {
            Value = initialValue;
            DoWork = doWork;
        }
    }
}