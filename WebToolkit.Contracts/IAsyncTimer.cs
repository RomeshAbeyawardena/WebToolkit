using System;
using System.Threading.Tasks;

namespace WebToolkit.Contracts
{
    public interface IAsyncTimer
    {
        event EventHandler Elapsed; 
        int Interval { get; }
        bool IsRunning { get; }
        Task Run(Func<IAsyncTimer,Task> asyncFunc = null);
        void SetInterval(int interval);
        void Stop();
    }
}