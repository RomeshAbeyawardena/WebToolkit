using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebToolkit.Common
{
    public class ParallelTask
    {
        public async Task<T1> Start<T, T1>(Task<T> method1, Func<Task<T>, Task<T1>> methodDelegate)
        {
            return await methodDelegate(method1);
        }

        public async Task<TResult> StartMany<TResult, TResult1>(Func<IEnumerable<Task<TResult1>>, Task<TResult>> methodDelegate,
            params Task<TResult1>[] pendingTasks)
        {
            return await methodDelegate(pendingTasks);
        }
    }
}