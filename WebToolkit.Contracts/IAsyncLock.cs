using System.Threading.Tasks;

namespace WebToolkit.Contracts
{
    public interface IAsyncLock
    {
        Task Invoke();
    }

    public interface IAsyncLock<TResult> : IAsyncLock
    {
        new Task<TResult> Invoke();
    }
}