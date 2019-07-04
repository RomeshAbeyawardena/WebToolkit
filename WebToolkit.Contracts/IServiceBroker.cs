using System.Collections.Generic;
using System.Reflection;

namespace WebToolkit.Contracts
{
    public interface IServiceBroker
    {
        IEnumerable<Assembly> GetServiceAssemblies();
    }
}