using System.Collections.Generic;
using Moq;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts.Providers;
using Xunit;

namespace WebToolkit.Tests
{
    public class ExtensionTests
    {
        [Fact]
        public void ValueOrDefault_returns_value_when_not_default()
        {
            var myTestValue = new MyTest().ValueOrDefault(false);

            Assert.IsNotType<bool>(myTestValue);
            Assert.IsType<MyTest>(myTestValue);
        }

        [Fact]
        public void ValueOrDefault_returns_default()
        {
            var myTestValue = default(MyTest).ValueOrDefault(false);

            Assert.IsNotType<MyTest>(myTestValue);
            Assert.IsType<bool>(myTestValue);
        }

        [Fact]
        public void String_ValueOrDefault_returns_value_when_not_default()
        {
            var myTestValue = "hello".ValueOrDefault(string.Empty);

            Assert.IsNotType<bool>(myTestValue);
            Assert.IsType<string>(myTestValue);
        }

        [Fact]
        public void String_ValueOrDefault_returns_default()
        {
            var myTestValue = default(string).ValueOrDefault(false);

            Assert.IsNotType<string>(myTestValue);
            Assert.IsType<bool>(myTestValue);
        }

        [Fact]
        public void IDictionary_AddRange()
        {
            var dictionaryMock 
                = new Mock<IDictionary<string, int>>();

            dictionaryMock.Setup(a => a.Add(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            dictionaryMock.Object.AddRange(
                new KeyValuePair<string, int>("meow", 1), 
                new KeyValuePair<string, int>("woof", 2),
                new KeyValuePair<string, int>("baa", 3));
            dictionaryMock.Verify(a => a.Add(It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(3));
        }

        [Fact]
        public void GetByteArray_calls_IEncodingProvider_GetBytes()
        {
            var encodingProviderMock = new Mock<IEncodingProvider>();
            encodingProviderMock.Setup(a=> a.GetBytes(It.IsAny<string>(), It.IsAny<Encoding>())).Verifiable();
            var sut = "MyTest";
            sut.GetBytes(encodingProviderMock.Object, Encoding.Ascii);
            encodingProviderMock.Verify(a=> a.GetBytes(It.IsAny<string>(), It.IsAny<Encoding>()), Times.Once);
        }

        [Fact]
        public void GetString_calls_IEncodingProvider_GetString()
        {
            var encodingProviderMock = new Mock<IEncodingProvider>();
            encodingProviderMock.Setup(a=> a.GetString(It.IsAny<byte[]>(), It.IsAny<Encoding>())).Verifiable();
            var sut = new byte[]{ 254, 129, 92 };
            sut.GetString(encodingProviderMock.Object, Encoding.Ascii);
            encodingProviderMock.Verify(a=> a.GetString(It.IsAny<byte[]>(), It.IsAny<Encoding>()), Times.Once);
        }

        internal class MyTest
        {
            private bool Value { get; set; }
        }
    }
}