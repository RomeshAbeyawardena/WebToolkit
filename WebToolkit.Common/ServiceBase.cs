using System;
using System.Threading.Tasks;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Data;

namespace WebToolkit.Common
{
    public class ServiceBase : IService
    {
        public T RetrieveAndAttach<TRepository, T>(TRepository repository, Func<T> retrievalCallback, bool attach) where TRepository : IRepository<T> where T : class
        {
            var retrievedEntry = retrievalCallback();
            if(retrievedEntry == null)
                return default;

            return attach 
                ? repository.Attach(retrievedEntry) 
                : retrievedEntry;
        }

        public async Task<T> RetrieveAndAttach<TRepository, T>(TRepository repository, Func<Task<T>> retrievalCallback, bool attach) where TRepository : IRepository<T> where T : class
        {
            var retrievedEntry = await retrievalCallback();
            if(retrievedEntry == null)
                return default;

            return attach 
                ? repository.Attach(retrievedEntry) 
                : retrievedEntry;
        }
    }
}