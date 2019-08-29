using System.Resources;
using WebToolkit.Common;
using WebToolkit.Common.Builders;
using Xunit;

namespace WebToolkit.Tests
{
    public class ResourceManagerProviderTests
    {
        [Fact]
        public void Bind()
        {
            var sut = ResourceManagerProvider.CreateResourceManagerProvider();
            var boundClass = sut.Bind<TestBindClass>(
                mockResourceDictionary: DictionaryBuilder
                    .CreateBuilder<string, object>()
                    .Add("Text", "Hello")
                    .Add("Number", 123456)
                    .Add("Amount", 225.89m)
                    .ToDictionary());

            Assert.Equal("Hello" ,boundClass.Text);
            Assert.Equal(123456 ,boundClass.Number);
            Assert.Equal(225.89m ,boundClass.Amount);
        }

        internal class TestBindClass
        {
            public string Text { get; set; }
            public int Number { get; set; }
            public decimal Amount { get; set; }
        }
    }
}