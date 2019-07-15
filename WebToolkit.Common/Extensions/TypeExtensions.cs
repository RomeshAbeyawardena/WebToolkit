using System;

namespace WebToolkit.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool ClassHasInterface<TInterface>(this Type type, Type concreteType = null)
        {
            if(!typeof(TInterface).IsInterface)
                throw new ArgumentException($"{nameof(TInterface)} must be an interface", nameof(TInterface));

            if(type == null)
                throw new ArgumentNullException(nameof(type));

            if(!type.IsClass)
                throw new ArgumentException($"{nameof(type)} must be an interface", nameof(type));

            if (concreteType == null)
                concreteType = typeof(TInterface);

            return type.IsClass && type.GetInterface(typeof(TInterface).Name) == concreteType;
        }
    }
}