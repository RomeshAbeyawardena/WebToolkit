using System;
using System.Threading.Tasks;

namespace WebToolkit.Shared
{
    public class AsyncLockOptions
    {
        public int ReleaseCount { get; set; }
        public int Initial { get; set; }
        public int Maximum { get; set; }
        public Func<Task> Task { get; set; }
    }

    public class AsyncLockOptions<TResult> : AsyncLockOptions
    {
        public new Func<Task<TResult>> Task { get; set; }
    }
}