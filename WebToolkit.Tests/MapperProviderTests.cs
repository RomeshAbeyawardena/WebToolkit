using System.Collections.Generic;
using AutoMapper;
using Moq;
using WebToolkit.Common.Providers;
using Xunit;

namespace WebToolkit.Tests
{
    public class MapperProviderTests
    {
        [Fact]
        public void Map_calls_AutoMapper_Map()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(a => a.Map<ClassA, ClassB>(It.IsAny<ClassA>())).Verifiable();
            var sut = new MapperProvider(mapperMock.Object);
            sut.Map<ClassA, ClassB>(new ClassA());
            mapperMock.Verify(a => a.Map<ClassA, ClassB>(It.IsAny<ClassA>()), Times.Once);
        }

        [Fact]
        public void IEnumerable_Map_calls_AutoMapper_Map()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(a => a.Map<IEnumerable<ClassA>, IEnumerable<ClassB>>(It.IsAny<IEnumerable<ClassA>>())).Verifiable();
            var sut = new MapperProvider(mapperMock.Object);
            sut.Map<ClassA, ClassB>(new List<ClassA>());
            mapperMock.Verify(a => a.Map<IEnumerable<ClassA>, IEnumerable<ClassB>>(It.IsAny<IEnumerable<ClassA>>()), Times.Once);
        }

        internal class ClassA
        {

        }

        internal class ClassB
        {

        }
    }
}