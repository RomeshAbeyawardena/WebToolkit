using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class TypeExtensionTests
    {
        [Fact]
        public void ClassHasInterface_returns_true()
        {
            Assert.True(typeof(TestInheritedClass).ClassHasInterface<ITestInterface>());
            Assert.True(typeof(TestInheritedClass).ClassHasInterface<ITestInterface>(typeof(ITestInterface)));
        }

        [Fact]
        public void ClassHasInterface_returns_false()
        {
            Assert.False(typeof(TestNonInheritedClass).ClassHasInterface<ITestInterface>());
            Assert.False(typeof(TestNonInheritedClass).ClassHasInterface<ITestInterface>(typeof(ITestInterface)));
        }

        private interface ITestInterface
        {
            
        }

        private class TestInheritedClass : ITestInterface
        {

        }

        private class TestNonInheritedClass
        {

        }
    }
}