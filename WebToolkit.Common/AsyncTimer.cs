using System;
using System.Threading;
using System.Threading.Tasks;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class AsyncTimer : IAsyncTimer
    {
        public event EventHandler Elapsed; 
        public int Interval { get; private set; }
        public bool IsRunning { get; private set; }
        public Task Run(Func<IAsyncTimer,Task> asyncCallback = null)
        {
            if(asyncCallback != null)
                _asyncCallback = asyncCallback;

            IsRunning = true;
            return Task.Run(_runInternal);
        }

        public void SetInterval(int interval)
        {
            Interval = interval;
        }

        public void Stop()
        {
            if(IsRunning)
                IsRunning = false;
        }

        public static IAsyncTimer Create(Func<IAsyncTimer,Task> asyncCallback, int interval)
        {
            return new AsyncTimer(asyncCallback, interval);
        }

        private Func<IAsyncTimer,Task> _asyncCallback;

        private async Task _runInternal()
        {
            while (IsRunning)
            {
                if (_asyncCallback == null) continue;

                await _asyncCallback?.Invoke(this);
                Elapsed?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(Interval);
            }
        }

        private AsyncTimer(Func<IAsyncTimer,Task> asyncCallback, int interval)
        {
            _asyncCallback = asyncCallback;
            Interval = interval;
        }
    }
}