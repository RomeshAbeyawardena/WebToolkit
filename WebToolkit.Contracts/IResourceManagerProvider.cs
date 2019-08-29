using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Resources;

namespace WebToolkit.Contracts
{
    public interface IResourceManagerProvider
    {
        ResourceManager ResourceManager { get; }
        T Bind<T>(CultureInfo cultureInfo = null, 
            IDictionary<string, object> mockResourceDictionary = null);
    }
}