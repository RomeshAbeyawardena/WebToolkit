using System;
using Moq;
using WebToolkit.Common;
using WebToolkit.Contracts;
using Xunit;

namespace WebToolkit.Tests
{
    public class DefaultCustomMapperTests
    {
        [Fact]
        public void Map_returns()
        {
            var customMapperSwitchMock = new Mock<ISwitch<string, Func<string, object>>>();
            customMapperSwitchMock.Setup(a => a.Case(It.IsAny<string>()))
                .Returns(a => a)
                .Verifiable();
            var sut = new DefaultCustomMapper<string, object, string>(customMapperSwitchMock.Object);
            sut.Map("test", "wind");
            customMapperSwitchMock.Verify(a => a.Case(It.IsAny<string>()), Times.Once);
            
        }
    }
}