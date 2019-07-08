using System;

namespace WebToolkit.Contracts
{
    public interface ILock
    {
        Action DoWork { get; }
        void Run();
    }

    public interface ILock<TResult> : ILock
    {
        new Func<TResult, TResult> DoWork { get; }
        TResult Value { get; }
    }
}