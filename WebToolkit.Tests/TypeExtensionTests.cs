using System;
using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class TypeExtensionTests
    {
        [Fact]
        public void ClassInherits_returns_true()
        {
            Assert.True(typeof(TestInheritedClass).ClassInherits<ITestInterface>());
            Assert.True(typeof(TestInheritedClass).ClassInherits<ITestInterface>(typeof(ITestInterface)));
        }

        [Fact]
        public void ClassInherits_returns_false()
        {
            Assert.False(typeof(TestNonInheritedClass).ClassInherits<ITestInterface>());
            Assert.False(typeof(TestNonInheritedClass).ClassInherits<ITestInterface>(typeof(ITestInterface)));
        }

        [Fact]
        public void TryGetCustomAttribute_returns()
        {
            var sut = typeof(TestNonInheritedClass2).GetPropertyByName("Value");
            var sut2 = typeof(TestNonInheritedClass).GetPropertyByName("Value");
            Assert.True(sut.TryGetCustomAttribute<IsAttribute>(out var isAttribute));
            Assert.IsType<IsAttribute>(isAttribute);
            Assert.False(sut2.TryGetCustomAttribute<IsAttribute>(out var isAttribute2));
            Assert.IsNotType<IsAttribute>(isAttribute2);
        }

        [Fact]
        public void GetCustomAttributeValue_returns()
        {
            var sut = typeof(TestNonInheritedClass2).GetPropertyByName("Value");

            Assert.True(sut.GetCustomAttributeValue<IsAttribute, bool>(a => a.IsActive));
        }

        private interface ITestInterface
        {
            bool Value { get; set; }
        }

        private class TestInheritedClass : ITestInterface
        {
            [Is(true)]
            public bool Value { get; set; }
        }

        private class IsAttribute : Attribute
        {
            public IsAttribute(bool isActive = false)
            {
                IsActive = isActive;
            }
            public bool IsActive { get; }
        }

        private class TestNonInheritedClass
        {
            public bool Value { get; set; }
        }

        private class TestNonInheritedClass2
        {
            [Is(true)]
            public bool Value { get; set; }
        }
    }
}