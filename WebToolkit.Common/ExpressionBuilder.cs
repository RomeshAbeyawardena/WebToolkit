using System.Linq.Expressions;

namespace WebToolkit.Common
{
    public static class ExpressionBuilder
    {
        public static dynamic Generate<TModel>(string memberName)
        {
            var valueExpressionParameter = Expression.Parameter(typeof(TModel), "model");
            var body = Expression.PropertyOrField(valueExpressionParameter, memberName);
            var convertedBody = Expression.Convert(body, typeof(object));
            return Expression.Lambda(convertedBody, valueExpressionParameter);
        }
    }
}