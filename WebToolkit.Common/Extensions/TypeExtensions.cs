using System;
using System.Reflection;

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

        public static bool TryGetCustomAttribute<TAttribute>(this PropertyInfo type, out TAttribute customAttribute)
            where TAttribute : Attribute
        {
            customAttribute = (TAttribute)type.GetCustomAttribute(typeof(TAttribute));
            
            return customAttribute != null;
        }

        public static T GetCustomAttributeValue<TAttribute, T>(this PropertyInfo type, Func<TAttribute, T> getValue)
            where TAttribute : Attribute
        {
            return 
                type.TryGetCustomAttribute<TAttribute>(out var customAttribute) 
                    ? getValue(customAttribute) 
                    : default;
        }
    }
}