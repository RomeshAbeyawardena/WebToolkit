using System;
using System.Linq;
using System.Reflection;

namespace WebToolkit.Common
{
    public static class SmartConvert
    {
        public static TDestination Convert<TSource, TDestination>(TSource source)
        {
            return SetProperties<TSource, TDestination>(source);
        }

        public static TDestination SetProperties<TSource, TDestination>(TSource source)
        {
            var sourceType = source.GetType();

            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            var destinationValue = Activator.CreateInstance<TDestination>();

            foreach (var destinationProperty in destinationProperties)
            {
                var convertMapAttribute = destinationProperty.GetCustomAttribute<ConvertFromAttribute>();

                var prop = sourceProperties.FirstOrDefault(a => a.Name == destinationProperty.Name
                                                                || (!string.IsNullOrEmpty(convertMapAttribute.MemberName)
                                                                && a.Name == convertMapAttribute.MemberName ));
                if (prop == null)
                    continue;

                destinationProperty.SetValue(destinationValue, prop.GetValue(source));
            }

            return destinationValue;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ConvertFromAttribute : Attribute
    {
        public string MemberName { get; }

        public ConvertFromAttribute(string memberName)
        {
            MemberName = memberName;
        }
    }
}