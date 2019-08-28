using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public class AsyncMessageQueue : IAsyncMessageQueue
    {
        public int Interval { get; set; }
        public bool IsRunning { get; private set; }

        public void Enqueue(AsyncMessage message)
        {
            _messageConcurrentQueue.Enqueue(message);
        }

        public void BeginProcessingQueue()
        {
            IsRunning = true;
            Task.Run(async () =>
            {
                while (IsRunning)
                {
                    if (!_messageConcurrentQueue.IsEmpty && _messageConcurrentQueue.TryDequeue(out var asyncMessage))
                        asyncMessage.OnCompletion(
                        await asyncMessage.Request(asyncMessage.RequestArguments));
                    Thread.Sleep(Interval);
                }
            });
        }

        public static IAsyncMessageQueue CreateMessageQueue(int interval)
        {
            return new AsyncMessageQueue(interval);
        }
        
        private AsyncMessageQueue(int interval)
        {
            _messageConcurrentQueue = new ConcurrentQueue<AsyncMessage>();
        }
        
        private readonly ConcurrentQueue<AsyncMessage> _messageConcurrentQueue;

        public void Dispose()
        {
            IsRunning = false;
        }
    }
}