using System.Collections.Generic;
using System.Threading.Tasks;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class LockableTests
    {
        [Fact]
        public void Run_returns()
        {
            var sut = new Lock<decimal>(100,a => a + 0.02m);

            var taskList = new List<Task>();
            for (var i = 0; i < 25; i++)
            {
                var task = new Task(() => sut.Run());
                task.Start();
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());
            Assert.Equal(100.5m, sut.Value);
        }
    }
}