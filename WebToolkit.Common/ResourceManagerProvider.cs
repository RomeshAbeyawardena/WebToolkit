using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class ResourceManagerProvider : IResourceManagerProvider
    {
        private ResourceManagerProvider(ResourceManager resourceManager = null)
        {
            ResourceManager = resourceManager;
        }

        public static IResourceManagerProvider CreateResourceManagerProvider(ResourceManager resourceManager = null)
        {
            return new ResourceManagerProvider(resourceManager);
        }

        public ResourceManager ResourceManager { get; }
        public T Bind<T>(CultureInfo cultureInfo = null, IDictionary<string, object> mockResourceDictionary = null)
        {
            var tType = typeof(T);

            var tTypeProperties = tType.GetProperties();

            var result = Activator.CreateInstance<T>();

            var useMockResourceDictionary = mockResourceDictionary != null;

            foreach (var tTypeProperty in tTypeProperties)
            {
                tTypeProperty.SetValue(result, useMockResourceDictionary 
                    ? mockResourceDictionary.TryGetValue(tTypeProperty.Name, out var value) 
                        ? value : null
                    : ResourceManager.GetObject(tTypeProperty.Name, cultureInfo));
            }

            return result;
        }
    }
}