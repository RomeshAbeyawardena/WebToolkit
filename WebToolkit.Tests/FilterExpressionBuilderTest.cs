using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class FilterExpressionBuilderTest
    {
        [Fact]
        public void GetFilterExpression()
        {
            var a = FilterExpressionBuilder.CreateBuilder<IncludeExpressionTests.TestClass>(
                new Dictionary<string, object>(new Dictionary<string, object>
                {
                    {"a","a"},
                    { "b", "b" },
                    { "c", 12345 }
                })).FilterExpression;
            
            Assert.IsAssignableFrom<Expression<Func<IncludeExpressionTests.TestClass, bool>>>(a);
        }
    }
}