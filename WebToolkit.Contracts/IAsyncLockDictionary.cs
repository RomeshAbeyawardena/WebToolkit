using System;
using System.Threading.Tasks;

namespace WebToolkit.Contracts
{
    public interface IAsyncLockDictionary
    {
        IAsyncLock GetOrCreate(string key, IAsyncLock asyncLock);
        IAsyncLock GetOrCreate(string key, Func<Task> action);
        IAsyncLock<T> GetOrCreate<T>(string key, Func<Task<T>> action);
    }
}