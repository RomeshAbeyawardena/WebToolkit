using System.Collections.Generic;
using WebToolkit.Common.Builders;
using Xunit;

namespace WebToolkit.Tests
{
    public class DictionaryBuilderTests
    {
        [Fact]
        public void Create_with_KeyValuePair_parameter_includes_existing_entries()
        {
            var sut = DictionaryBuilder<int, string>.Create(new[]
            {
                new KeyValuePair<int, string>(1, "A"), 
                new KeyValuePair<int, string>(2, "B"),
                new KeyValuePair<int, string>(3, "C"),
                new KeyValuePair<int, string>(4, "D"), 
            });

            Assert.Contains(1, sut.ToDictionary());
            Assert.Contains(2, sut.ToDictionary());
            Assert.Contains(3, sut.ToDictionary());
            Assert.Contains(4, sut.ToDictionary());
        }

        [Fact]
        public void ContainsKey_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(new[]
            {
                new KeyValuePair<int, string>(1, "A"), 
                new KeyValuePair<int, string>(2, "B"),
                new KeyValuePair<int, string>(3, "C"),
                new KeyValuePair<int, string>(4, "D"), 
            });

            Assert.True(sut.ContainsKey(4));
            Assert.False(sut.ContainsKey(6));
        }

        
        [Fact]
        public void Contains_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(new[]
            {
                new KeyValuePair<int, string>(1, "A"), 
                new KeyValuePair<int, string>(2, "B"),
                new KeyValuePair<int, string>(3, "C"),
                new KeyValuePair<int, string>(4, "D"), 
            });

            Assert.True(sut.Contains(new KeyValuePair<int, string>(4, "D")));
            Assert.False(sut.Contains(new KeyValuePair<int, string>(5, "D")));
        }
    }
}