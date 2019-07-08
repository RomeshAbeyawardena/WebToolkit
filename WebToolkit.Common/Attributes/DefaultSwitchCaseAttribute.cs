using System;

namespace WebToolkit.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DefaultSwitchCaseAttribute : Attribute
    {
        public Func<object> Value { get; }

        public DefaultSwitchCaseAttribute(Func<object> value)
        {
            Value = value;
        }
    }
}