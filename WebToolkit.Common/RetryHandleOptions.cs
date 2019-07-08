using System;
using System.Collections.Generic;

namespace WebToolkit.Common
{
    public class RetryHandleOptions
    {
        public int Timeout { get; set; }
        public int MaximumAttempts { get; set; }
        public IEnumerable<Type> ExceptionTypes { get; set; }
    }
}