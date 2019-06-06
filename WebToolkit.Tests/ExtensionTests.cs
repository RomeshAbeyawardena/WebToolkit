using WebToolkit.Common.Extensions;
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
            var myTestValue = "hello".ValueOrDefault(false);

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

        internal class MyTest
        {
            private bool Value { get; set; }
        }
    }
}