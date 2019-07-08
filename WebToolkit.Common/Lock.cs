using System;
using System.Threading;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public sealed class Lock : ILock
    {
        public Action DoWork { get; }
        public void Run()
        {
            lock(_lockObject)
                DoWork();
        }

        public static Lock Create(Action doWork)
        {
            return new Lock(doWork);
        }

        public static Lock<TResult> Create<TResult>(TResult initialValue, Func<TResult, TResult> doWork)
        {
            return Lock<TResult>.Create(initialValue, doWork);
        }

        private readonly object _lockObject = new object();
        private Lock(Action doWork)
        {
            DoWork = doWork;
        }
    }

    public sealed class Lock<TResult> : ILock<TResult>
    {
        private readonly object _lockObject = new object();
        public static Lock<TResult> Create(TResult initialValue, Func<TResult, TResult> doWork)
        {
            return new Lock<TResult>(initialValue, doWork);
        }

        public TResult Value { get; private set; }

        Action ILock.DoWork => throw new NotSupportedException();

        public Func<TResult, TResult> DoWork { get; }
        public void Run()
        {
            lock (_lockObject)
            {
                Value = DoWork(Value);
            }
        }

        private Lock(TResult initialValue, Func<TResult, TResult> doWork)
        {
            Value = initialValue;
            DoWork = doWork;
        }
    }
}