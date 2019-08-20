using System;
using System.Threading.Tasks;
using WebToolkit.Contracts.Data;

namespace WebToolkit.Contracts
{
    public interface IService
    {
        T RetrieveAndAttach<TRepository, T>(TRepository repository, Func<T> retrievalCallback, bool attach)
            where TRepository : IRepository<T>
            where T: class;

        Task<T> RetrieveAndAttach<TRepository, T>(TRepository repository, Func<Task<T>> retrievalCallback, bool attach)
            where TRepository : IRepository<T>
            where T: class;
    }
}