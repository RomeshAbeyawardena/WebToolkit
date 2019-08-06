using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using WebToolkit.Common;
using WebToolkit.Contracts.Providers;
using Xunit;

namespace WebToolkit.Tests
{
    public class MapExpressionTest
    {
        [Fact]
        public void Convert()
        {
            var expected = new B[]
            {
                new B {
                Context = int.MinValue,
                Name = "Mapped",
                Value = int.MaxValue
                }
            };

            var actual = new []
            {
                new A {
                Context = int.MinValue,
                Name = "Mapped",
                Value = int.MaxValue
                }
            };

            var context = new object();

            var mapperProviderMock = new Mock<IMapperProvider>();
            mapperProviderMock
                    .Setup(a => a.MapArray(It.IsAny<IEnumerable<A>>(), typeof(A), typeof(B)))
                    .Returns(expected)
                    .Verifiable();
            var result =  MapExpression.Convert<MyContainer<A>, MyContainer<B>>(new MyContainer<A>
            {
                Context = context,
                Value = 12345,
                Name = "Axe",
                Result = actual
            }, () => mapperProviderMock.Object);
            mapperProviderMock.Verify(a => a.MapArray(It.IsAny<IEnumerable<A>>(),typeof(A), typeof(B)), Times.Once);
            
            Assert.Equal("Axe", result.Name);
            Assert.Equal(12345, result.Value);
            Assert.Equal(context, result.Context);
            Assert.Same(expected, result.Result);
        }

        internal class A
        {
            public int Value { get; set; }
            public string Name { get; set; }
            public object Context { get; set; }
        }

        internal class B
        {
            public int Value { get; set; }
            public string Name { get; set; }
            public object Context { get; set; }
        }

        internal class MyContainer<TModel>
        {
            public int Value { get; set; }
            public string Name { get; set; }
            public object Context { get; set; }
            public IEnumerable<TModel> Result { get; set; }
        }
    }
}