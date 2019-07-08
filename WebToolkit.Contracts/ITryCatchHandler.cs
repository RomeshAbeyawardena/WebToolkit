using System;
using System.Threading.Tasks;

namespace WebToolkit.Contracts
{
    public interface ITryCatchHandler<T>
    {
        T Handle(Func<T> tryFunc, Action<Exception> catchFunc, Action finallyAction = null,
            params Type[] catchExceptionTypesArray);

        Task<T> HandleAsync(Func<Task<T>> tryFunc, Func<Exception, Task> catchFunc, Func<Task> finallyFunc = null,
            params Type[] catchExceptionTypesArray);
    }
}