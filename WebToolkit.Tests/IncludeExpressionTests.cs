using System;
using System.Linq.Expressions;
using WebToolkit.Shared;
using WebToolkit.Tests.Models;
using Xunit;

namespace WebToolkit.Tests
{
    public class IncludeExpressionTests
    {
        [Fact]
        public void Value_Returns()
        {
            
            var sut = IncludeExpressionBuilder<TestClass>
                .CreateBuilder()
                .AddExpression(a => a.A)
                .AddExpression(a => a.B)
                .AddExpression(a => a.C)
                .AddExpression(a => a.D);

            var array = sut.ToArray();
            foreach (var includeExpression in array)
            {
                var testClassType = typeof(TestClass);
                var keyType = includeExpression.GetType().GenericTypeArguments[1];
                var genericIncludeExpressionType = typeof(IncludeExpression<,>)
                    .MakeGenericType(testClassType, keyType);

                var genericFunc = typeof(Func<,>).MakeGenericType(testClassType, typeof(object));
                var genericExpression = typeof(Expression<>).MakeGenericType(genericFunc);
                
                Assert.IsType(genericIncludeExpressionType, includeExpression);
                Assert.IsAssignableFrom(genericExpression, includeExpression.Value);
            }
        }

        internal class TestClass
        {
            public string A { get; set; }
            public object B { get; set; }
            public int C { get; set; }
            public decimal D { get; set; }
        }
    }
}