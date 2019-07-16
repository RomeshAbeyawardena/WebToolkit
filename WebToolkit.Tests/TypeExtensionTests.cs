using System;
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

        [Fact]
        public void TryGetCustomAttribute_returns()
        {
            var sut = typeof(TestNonInheritedClass).GetProperties()[0];
            var sut2 = typeof(TestInheritedClass).GetProperties()[0];
            Assert.True(sut.TryGetCustomAttribute<IsAttribute>(out var isAttribute));
            Assert.IsType<IsAttribute>(isAttribute);
            Assert.False(sut2.TryGetCustomAttribute<IsAttribute>(out var isAttribute2));
            Assert.IsNotType<IsAttribute>(isAttribute2);
        }

        private interface ITestInterface
        {
            bool Value { get; set; }
        }

        private class TestInheritedClass : ITestInterface
        {
            public bool Value { get; set; }
        }

        private class IsAttribute : Attribute
        {

        }

        private class TestNonInheritedClass
        {
            [Is]
            public bool Value { get; set; }
        }
    }
}