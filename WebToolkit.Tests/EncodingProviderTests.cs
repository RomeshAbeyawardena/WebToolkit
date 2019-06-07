using System;
using Moq;
using WebToolkit.Common;
using WebToolkit.Common.Providers;
using WebToolkit.Contracts;
using Xunit;
using Encoding = WebToolkit.Contracts.Encoding;

namespace WebToolkit.Tests
{
    public class EncodingProviderTests
    {
        [Fact]
        public void GetEncoding_calls_Switch_Case()
        {
            var encodingSwitchMock = new Mock<ISwitch<Encoding, System.Text.Encoding>>();
            encodingSwitchMock.Setup(a => a.Case(It.IsAny<Encoding>())).Verifiable();

            var sut = new EncodingProvider(encodingSwitchMock.Object);
            sut.GetEncoding(Encoding.Ascii);
            encodingSwitchMock.Verify(a => a.Case(It.IsAny<Encoding>()), Times.Once);
        }

        [Fact]
        public void Constructor_creates_new_Switch_when_no_parameters_are_passed()
        {
            var sut = new EncodingProvider();

            Assert.NotNull(sut.EncodingSwitch);
        }

        [Fact]
        public void GetBytes_throws_ArgumentException_when_case_returns_null()
        {
            var sut = new EncodingProvider(Switch<Encoding, System.Text.Encoding>.Create(defaultValueExpression: () => null));
            Assert.Throws<ArgumentException>(() => sut.GetBytes("Test", Encoding.Ascii));
        }

        [Fact]
        public void GetBytes_returns()
        {
            var sut = new EncodingProvider();
            var result = sut.GetBytes("test", Encoding.Ascii);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetString_returns()
        {
            var sut = new EncodingProvider();
            var result = sut.GetString(new byte[]{ 32, 144, 122 }, Encoding.Ascii);
            Assert.NotNull(result);
        }
        
        [Fact]
        public void GetString_throws_ArgumentException_when_case_returns_null()
        {
            var sut = new EncodingProvider(Switch<Encoding, System.Text.Encoding>.Create(defaultValueExpression: () => null));
            Assert.Throws<ArgumentException>(() => sut.GetString(new byte[2], Encoding.Ascii));
        }
    }
}