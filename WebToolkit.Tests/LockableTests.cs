using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
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
            var sut = Lock.Create<decimal>(100,a => a + 0.02m);

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

        [Fact]
        public void TResult_Run_returns()
        {
            var sut = Lock.Create<decimal>(100,a => a + 0.02m);

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

        [Fact]
        public void void_Run_returns()
        {
            var lockTaskCount = 0;
            var sut = Lock.Create(() => lockTaskCount++);

            var taskList = new List<Task>();
            for (var i = 0; i < 25; i++)
            {
                var task = new Task(() => sut.Run());
                task.Start();
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());
            Assert.Equal(25, lockTaskCount);
        }
    }
}