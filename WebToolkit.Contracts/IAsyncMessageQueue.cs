using System;
using System.Threading.Tasks;
using WebToolkit.Shared;

namespace WebToolkit.Contracts
{
    public interface IAsyncMessageQueue : IDisposable
    {
        int Interval { get; set; }
        bool IsRunning { get; }
        void Enqueue(AsyncMessage message);
        void BeginProcessingQueue();
    }
}