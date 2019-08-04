using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace WebToolkit.Shared
{
    public static class MapExpression
    {
        public static TDestinationContainer Convert<TSourceContainer, TDestinationContainer>(TSourceContainer container, 
            Func<IMapper> mapper)
        {
            var sourceContainerType = typeof(TSourceContainer);
            var enumerableType = typeof(IEnumerable<>);
            var destinationContainerType = typeof(TDestinationContainer);
            var sourceType = sourceContainerType.GenericTypeArguments[0];
            var sourceEnumerableType = enumerableType.MakeGenericType(sourceType);
            var destinationType = destinationContainerType.GenericTypeArguments[0];
            var destinationEnumerableType = enumerableType.MakeGenericType(sourceType);

            var sourceProps = sourceContainerType.GetProperties(); 
            var sourceProp = sourceProps.FirstOrDefault(p => p.PropertyType == sourceType || p.PropertyType == sourceEnumerableType);
            
            if(sourceProp == null)
                throw new NullReferenceException("Source property not found");

            var mappedValue = mapper().Map(sourceProp.GetValue(container), sourceType, destinationType);

            var destinationContainer =  (TDestinationContainer)Activator.CreateInstance(destinationContainerType);

            var destinationProp = destinationContainerType.GetProperty(sourceProp.Name);

            if(destinationProp == null)
                throw new NullReferenceException("Source property not found");

            destinationProp.SetValue(destinationContainer, mappedValue);

            return destinationContainer;
        }
    }
}