using System;
using System.Linq.Expressions;
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
                builder =>
                {
                    builder.Add("b", FilterExpressionParameter.Create(f =>
                    {
                        f.Operator = Operator.And;
                        f.Value = "Test";
                        f.ComparisonType = ComparisonType.Equal;
                    }));
                    builder.Add("a", FilterExpressionParameter.Create(f =>
                    {
                        f.Operator = Operator.Not;
                        f.Value = "Test21321";
                        f.ComparisonType = ComparisonType.Contains;
                    }));
                    builder.Add("c", FilterExpressionParameter.Create(f =>
                    {
                        f.Operator = Operator.Or;
                        f.Value = 64571;
                        f.ComparisonType = ComparisonType.LessThan;
                    }));
                }).FilterExpression;

            Assert.IsAssignableFrom<Expression<Func<IncludeExpressionTests.TestClass, bool>>>(a);
        }

        [Fact]
        public void GetFilterExpressionForNullable()
        {
            int? value = 4567;
            var a = FilterExpressionBuilder.CreateBuilder<MyTestClass>(builder =>
            {
                builder.Add("a", FilterExpressionParameter.Create(fep =>
                {
                    fep.Value = value;
                    fep.ComparisonType = ComparisonType.Equal;
                    fep.Operator = Operator.Or;
                })).Add("b", FilterExpressionParameter.Create(fep =>
                {
                    fep.Value = value;
                    fep.ComparisonType = ComparisonType.Equal;
                    fep.Operator = Operator.Or;
                }));
            }).FilterExpression;

            Assert.IsAssignableFrom<Expression<Func<MyTestClass, bool>>>(a);
        }

        internal class MyTestClass
        {
            public int? A { get; set; }
            public int? B { get; set; }
        }
    }
}