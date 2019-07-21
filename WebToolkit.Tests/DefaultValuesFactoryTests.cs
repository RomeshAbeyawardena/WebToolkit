using System;
using Moq;
using WebToolkit.Common;
using WebToolkit.Contracts.Providers;
using WebToolkit.Tests.Models;
using Xunit;

namespace WebToolkit.Tests
{
    public class DefaultValuesFactoryTests
    {
        [Fact]
        public void Assign_calls_DefaultValuesProvider_assign_method()
        {
            var myTestClass = new MyTestClass();
            var defaultValueProviderForMyTestClassMock = new Mock<IDefaultValueProvider<MyTestClass>>();
            defaultValueProviderForMyTestClassMock
                .Setup(dvp =>
                    dvp.Assign(It.IsAny<object>()))
                .Verifiable();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IDefaultValueProvider<MyTestClass>)))
                .Returns(defaultValueProviderForMyTestClassMock.Object)
                .Verifiable();

            var sut = new DefaultValuesFactory(serviceProviderMock.Object);
            
            sut.Assign(myTestClass);

            serviceProviderMock.Verify(a => a.GetService(typeof(IDefaultValueProvider<MyTestClass>)), Times.Once);
            defaultValueProviderForMyTestClassMock.Verify(a => a.Assign((object)myTestClass));
        }

        [Fact]
        public void Assign_throws_when_provider_is_undefined()
        {
            var myTestClass = new MyTestClass();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var sut = new DefaultValuesFactory(serviceProviderMock.Object);
            Assert.Throws<NullReferenceException>(() => sut.Assign(myTestClass));
        }
    }
}