using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebToolkit.Common
{
    public class RetryHandler
    {
        private RetryHandler(Action<RetryHandleOptions> retryOptionsAction, params Type[] exceptionTypes)
        {
            Options = new RetryHandleOptions {ExceptionTypes = exceptionTypes};
            retryOptionsAction(Options);
        }

        public RetryHandleOptions Options { get; }

        public async Task<T> HandleAsync<T>(Func<Task<T>> tryFunc, Action<Exception> catchFunc = null, Func<Task> finallyFunc = null)
        {
            T result = default;
            var attempts = 0;

            var success = false;
            while (!success && attempts < Options.MaximumAttempts)
                result = await TryCatchHandler.HandleAsync(async() =>
                {
                    var value = await tryFunc();
                    success = true;
                    return value;
                } , async(ex) =>
                {
                    await Task.CompletedTask;
                    catchFunc?.Invoke(ex);
                    Thread.Sleep(Options.Timeout);
                    attempts++;
                }, finallyFunc, catchExceptionTypesArray: Options.ExceptionTypes.ToArray());

            return result;
        }

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

        public static RetryHandler Create(Action<RetryHandleOptions> retryOptionsAction, params Type[] exceptionTypes)
        {
            return new RetryHandler(retryOptionsAction, exceptionTypes);
        }
    }
}