using System;

namespace WebToolkit.Common
{
    public class DbContextExtendedOptions
    {
        public DbContextExtendedOptions(Action<DbContextExtendedOptions> options)
        {
            options(this);
        }

        public bool SingulariseTableNames { get; set; }
        public bool DetachOnUpdate { get; set; }
    }
}