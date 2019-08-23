using System;

namespace WebToolkit.Shared
{
    public enum Operator { And, Or, Not }
    [Flags]
    public enum ComparisonType { Equal = 1, LessThan = 2, GreaterThan = 4, Contains = 8 }
    public class FilterExpressionParameter
    {
        public static FilterExpressionParameter Create(Action<FilterExpressionParameter> filterExpressionParameter)
        {
            var newFilterExpressionParameter = new FilterExpressionParameter();
            filterExpressionParameter(newFilterExpressionParameter);
            return newFilterExpressionParameter;
        }

        private FilterExpressionParameter()
        {
            
        }

        public object Value { get; set; }
        public Operator Operator { get; set; }
        public ComparisonType ComparisonType { get; set; }
        public Type ValueType => Value.GetType();

    }
}