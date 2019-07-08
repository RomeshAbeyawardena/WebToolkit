using System;
using System.Linq;
using System.Threading;

namespace WebToolkit.Common
{
    public class RetryHandle
    {
        private RetryHandle(Action<RetryHandleOptions> retryOptionsAction, params Type[] exceptionTypes)
        {
            Options = new RetryHandleOptions {ExceptionTypes = exceptionTypes};
            retryOptionsAction(Options);
        }

        public RetryHandleOptions Options { get; }

        public T Handle<T>(Func<T> tryFunc, Action<Exception> catchFunc = null, Action finallyFunc = null)
        {
            T result = default;
            var attempts = 0;

            var success = false;
            while (!success && attempts < Options.MaximumAttempts)
                result = TryCatchHandler.Handle(() =>
                {
                    var value = tryFunc();
                    success = true;
                    return value;
                } , ex =>
                {
                    catchFunc?.Invoke(ex);
                    Thread.Sleep(Options.Timeout);
                    attempts++;
                }, finallyFunc, catchExceptionTypesArray: Options.ExceptionTypes.ToArray());

            return result;
        }

        public static RetryHandle Create(Action<RetryHandleOptions> retryOptionsAction, params Type[] exceptionTypes)
        {
            return new RetryHandle(retryOptionsAction, exceptionTypes);
        }
    }
}