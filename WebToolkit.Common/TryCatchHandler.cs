using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebToolkit.Common
{
    public static class TryCatchHandler
    {
        public static T Handle<T>(Func<T> tryFunc, Action<Exception> catchFunc, Action finallyAction = null, params Type[] catchExceptionTypesArray)
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
            }
            finally
            {
                finallyAction?.Invoke();
            }

            return default;
        }
        
        public static async Task<T> HandleAsync<T>(Func<Task<T>> tryFunc, Func<Exception, Task> catchFunc, Func<Task> finallyFunc = null, params Type[] catchExceptionTypesArray)
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