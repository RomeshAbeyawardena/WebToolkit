using System;
using WebToolkit.Common;
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

        [Fact]
        public void Apply_applies_action_on_target_type()
        {
            var myTypeSwitch = Switch.Create<Type, object>()
                .CaseWhen(typeof(int), 12345)
                .CaseWhen(typeof(string), "Test")
                .CaseWhen(typeof(decimal), 1.234m)
                .CaseWhen(typeof(float), 31.23f)
                .CaseWhen(typeof(bool), false)
                .CaseWhenDefault(Switch.Default);

            var myApplicableClass = new MyApplicableClass();

            var myApplicableClassType = typeof(MyApplicableClass);

            myApplicableClassType.ApplyAll( myApplicableClass, (a, target) => a.SetValue(target, myTypeSwitch.Case(a.PropertyType)));

            Assert.Equal(myTypeSwitch.Case(typeof(int)), myApplicableClass.MyInt);
            Assert.Equal(myTypeSwitch.Case(typeof(string)), myApplicableClass.MyString);
            Assert.Equal(myTypeSwitch.Case(typeof(decimal)), myApplicableClass.MyDecimal);
            Assert.Equal(myTypeSwitch.Case(typeof(float)), myApplicableClass.MyFloat);
            Assert.Equal(myTypeSwitch.Case(typeof(bool)), myApplicableClass.MyBool);

            myApplicableClassType.Apply((a, target) => a.SetValue(target, 12394), myApplicableClass, "AnotherValue");
            Assert.Equal(12394, myApplicableClass.AnotherValue);

            myApplicableClass.Apply((a, target) => a.SetValue(target, 42321), a => a.AnotherValue);
            Assert.Equal(42321, myApplicableClass.AnotherValue);
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

        private class MyApplicableClass
        {
            public int MyInt { get; set; }
            public string MyString { get; set; }
            public decimal MyDecimal { get; set; }
            public float MyFloat { get; set; }
            public bool MyBool { get; set; }
            public double AnotherValue { get; set; }
        }
    }
}