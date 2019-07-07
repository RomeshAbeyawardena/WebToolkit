using System;
using System.Threading.Tasks;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class TryCatchHandlerTests
    {
        [Fact]
        public void TryCatchHandler_Handle()
        {
            Assert.Equal(default, TryCatchHandler.Handle<bool>(() => throw new NotSupportedException(),
                ex => Assert.Equal(typeof(NotSupportedException), ex.GetType()), catchExceptionTypesArray: typeof(NotSupportedException)));
        }

        [Fact]
        public void TryCatchHandler_HandleAsync()
        {
            Assert.Equal(default, TryCatchHandler.HandleAsync<bool>(() => throw new NotSupportedException(),
                ex =>
                {
                    Assert.Equal(typeof(NotSupportedException), ex.GetType());
                    return Task.FromResult(true);
                }, catchExceptionTypesArray: typeof(NotSupportedException)).Result);
        }
    }
}