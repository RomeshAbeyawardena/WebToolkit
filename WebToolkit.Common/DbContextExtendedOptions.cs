using System;

namespace WebToolkit.Common
{
    public class DbContextExtendedOptions
    {
        public bool SingulariseTableNames { get; set; }
        public bool DetachOnUpdate { get; set; }
    }
}