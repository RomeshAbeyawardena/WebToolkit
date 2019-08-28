using System.Threading;
using System.Threading.Tasks;
using WebToolkit.Common;
using WebToolkit.Shared;
using Xunit;

namespace WebToolkit.Tests
{
    public class AsyncMessageQueueTests
    {
        [Fact]
        public void BeginProcessingQueue()
        {
            using (var sut = AsyncMessageQueue.CreateMessageQueue(500))
            {
                sut.BeginProcessingQueue();
                sut.Enqueue(new AsyncMessage(12345, Request, 
                    OnCompletion));
                Thread.Sleep(3000);
            } 
        }

        private async Task<object> Request(object args)
        {
            return await Task.FromResult(args);
        }
        
        private void OnCompletion(object result)
        {
            Assert.Equal(512345, result);
        }
    }
}