using System;
using System.Linq;
using System.Threading.Tasks;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class TryCatchHandler<T> : ITryCatchHandler<T>
    {
        public ITryCatchHandler<T> Create()
        {
            return new TryCatchHandler<T>();
        }

        public T Handle(Func<T> tryFunc, Action<Exception> catchFunc, Action finallyAction = null, params Type[] catchExceptionTypesArray)
        {
            return TryCatchHandler.Handle(tryFunc, catchFunc, finallyAction, catchExceptionTypesArray: catchExceptionTypesArray);
        }

        public Task<T> HandleAsync(Func<Task<T>> tryFunc, Func<Exception, Task> catchFunc, Func<Task> finallyFunc = null, params Type[] catchExceptionTypesArray)
        {
            return TryCatchHandler.HandleAsync(tryFunc, catchFunc, finallyFunc, catchExceptionTypesArray: catchExceptionTypesArray);
        }

        private TryCatchHandler()
        {
            
        }
    }

    public static class TryCatchHandler
    {
        public static T Handle<T>(Func<T> tryFunc, Action<Exception> catchFunc, Action finallyAction = null, bool throwUnhandled = true, params Type[] catchExceptionTypesArray)
        {
            try
            {
                return tryFunc();
            }
            catch (Exception ex)
            {
                if (catchExceptionTypesArray.Any(catchExceptionType => ex.GetType() == catchExceptionType))
                {
                    catchFunc(ex);
                }
                else if(throwUnhandled) 
                    throw;
            }
            finally
            {
                finallyAction?.Invoke();
            }

            return default;
        }
        
        public static async Task<T> HandleAsync<T>(Func<Task<T>> tryFunc, Func<Exception, Task> catchFunc, Func<Task> finallyFunc = null, bool throwUnhandled = true, params Type[] catchExceptionTypesArray)
        {
            try
            {
                return await tryFunc();
            }
            catch (Exception ex)
            {
                if (catchExceptionTypesArray.Any(catchExceptionType => ex.GetType() == catchExceptionType))
                {
                    await catchFunc(ex);
                }
                else if(throwUnhandled) 
                    throw;
            }
            finally
            {
                if (finallyFunc != null) 
                    await finallyFunc.Invoke();
            }

            return default;
        }
    }
}