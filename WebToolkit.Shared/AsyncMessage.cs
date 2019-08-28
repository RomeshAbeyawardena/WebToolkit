using System;
using System.Threading.Tasks;

namespace WebToolkit.Shared
{
    public class AsyncMessage
    {
        public AsyncMessage(object arguments, Func<object, Task<object>> request, Action<object> onCompletion)
        {
            RequestArguments = arguments;
            Request = request;
            OnCompletion = onCompletion;
        }

        public Action<object> OnCompletion { get; }
        public Func<object, Task<object>> Request { get; }
        public object RequestArguments { get; }
    }
}