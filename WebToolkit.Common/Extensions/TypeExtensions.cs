using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebToolkit.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool ClassInherits<TInterface>(this Type type, Type concreteType = null)
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

        public static TSelector GetCustomAttributeValue<TAttribute, TSelector>(this PropertyInfo type, Func<TAttribute, TSelector> getValue = null)
            where TAttribute : Attribute
        {
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return 
                type.TryGetCustomAttribute<TAttribute>(out var customAttribute) 
                    ? getValue(customAttribute) 
                    : default;
        }

        /// <summary>
        /// Applies multiple actions on a single property of a target type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="applyAction"></param>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static PropertyInfo Apply(this Type type, Action<PropertyInfo, object> applyAction, object target, string propertyName = null, PropertyInfo propertyInfo = null)
        {
            var property = string.IsNullOrEmpty(propertyName) 
                ? propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo)) 
                : GetPropertyByName(type, propertyName);

            if(property == null)
                throw new ArgumentNullException(nameof(propertyName));

            applyAction(property, target);

            return property;
        }

        /// <summary>
        /// Applies multiple actions on all properties of a target type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target"></param>
        /// <param name="applyAction"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> ApplyAll(this Type type, object target, Action<PropertyInfo, object> applyAction)
        {
            var properties = type.GetProperties();

            return properties
                .Select(property => 
                    Apply(type, applyAction, target, propertyInfo: property))
                .ToArray();
        }

        public static PropertyInfo GetPropertyByName(this Type type, string propertyName)
        {
            return type
                .GetProperties()
                .FirstOrDefault(property => property.Name == propertyName);
        }
    }
}