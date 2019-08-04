using AutoMapper;
using Moq;
using WebToolkit.Shared;
using Xunit;

namespace WebToolkit.Tests
{
    public class MapExpressionTest
    {
        [Fact]
        public void Convert()
        {
            var expected = new B
            {
                Context = int.MinValue,
                Name = "Mapped",
                Value = int.MaxValue
            };

            var actual = new A
            {
                Context = int.MinValue,
                Name = "Mapped",
                Value = int.MaxValue
            };

            var mapperMock = new Mock<IMapper>();
                mapperMock
                    .Setup(a => a.Map(It.IsAny<A>(), typeof(A), typeof(B)))
                    .Returns(expected)
                    .Verifiable();
            var result =  MapExpression.Convert<MyContainer<A>, MyContainer<B>>(new MyContainer<A>
            {
                Result = actual
            }, () => mapperMock.Object);
            mapperMock.Verify(a => a.Map(It.IsAny<A>(),typeof(A), typeof(B)), Times.Once);
            
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
            public TModel Result { get; set; }
        }
    }
}