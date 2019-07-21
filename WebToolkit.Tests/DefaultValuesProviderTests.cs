using WebToolkit.Common.Providers;
using WebToolkit.Tests.Models;
using Xunit;

namespace WebToolkit.Tests
{
    public class DefaultValuesProviderTests
    {
        [Fact]
        public void Assign_calls_Defaults_action()
        {
            var myTestClass = new MyTestClass();
            var sut = DefaultValuesProvider<MyTestClass>.Create(m =>
            {
                m.HasChanged = true;
            });

            sut.Assign(myTestClass);
            Assert.True(myTestClass.HasChanged);
        }

        [Fact]
        public void Assign_object_calls_Defaults_action()
        {
            var myTestClass = new MyTestClass();
            var sut = DefaultValuesProvider<MyTestClass>.Create(m =>
            {
                m.HasChanged = true;
            });

            sut.Assign((object)myTestClass);
            Assert.True(myTestClass.HasChanged);
        }
    }
}