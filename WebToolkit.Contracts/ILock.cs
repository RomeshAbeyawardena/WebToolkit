using System;

namespace WebToolkit.Contracts
{
    public interface ILock<TResult>
    {
        Func<TResult, TResult> DoWork { get; }
        TResult Value { get; }

        void Run();
    }
}