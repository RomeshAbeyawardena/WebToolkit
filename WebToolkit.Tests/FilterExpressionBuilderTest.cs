using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using WebToolkit.Common;
using WebToolkit.Shared;
using Xunit;

namespace WebToolkit.Tests
{
    public class FilterExpressionBuilderTest
    {
        [Fact]
        public void GetFilterExpression()
        {
            var a = FilterExpressionBuilder.CreateBuilder<IncludeExpressionTests.TestClass>(
                new Dictionary<string, FilterExpressionParameter>
                {
                    {"b", FilterExpressionParameter.Create(f =>
                    {
                        f.Operator = Operator.And;
                        f.Value = "Test";
                        f.ComparisonType = ComparisonType.Equal;
                    })},
                    { "a", FilterExpressionParameter.Create(f => {
                        f.Operator = Operator.Not;
                        f.Value = "Test21321";
                        f.ComparisonType = ComparisonType.Contains;
                    })},
                    { "c", FilterExpressionParameter.Create(f => {
                        f.Operator = Operator.Or;
                        f.Value = 64571;
                        f.ComparisonType = ComparisonType.LessThan;
                    })}
                    }).FilterExpression;

            Assert.IsAssignableFrom<Expression<Func<IncludeExpressionTests.TestClass, bool>>>(a);
        }
    }
}