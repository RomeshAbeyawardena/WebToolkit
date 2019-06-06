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
            var sut = DictionaryBuilder<int, string>.Create(Setup());

            Assert.Contains(1, sut.ToDictionary());
            Assert.Contains(2, sut.ToDictionary());
            Assert.Contains(3, sut.ToDictionary());
            Assert.Contains(4, sut.ToDictionary());
        }

        [Fact]
        public void Create_with_empty_parameter_generates_empty_list()
        {
            var sut = DictionaryBuilder<string, int>.Create();
            Assert.Empty(sut);
        }

        [Fact]
        public void ContainsKey_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(Setup());

            Assert.True(sut.ContainsKey(4));
            Assert.False(sut.ContainsKey(6));
        }

        [Fact]
        public void Contains_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(Setup());

            Assert.True(sut.Contains(new KeyValuePair<int, string>(4, "D")));
            Assert.False(sut.Contains(new KeyValuePair<int, string>(5, "D")));
        }

        [Fact]
        public void ToKeyValuePairs_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(Setup());
            Assert.Equal(Setup(), sut.ToKeyValuePairs());
        }

        [Fact]
        public void Indexer_returns()
        {
            var sut = DictionaryBuilder<int, string>.Create(Setup());
            Assert.Equal("A", sut[1]);
        }

        private IEnumerable<KeyValuePair<int, string>> Setup()
        {
            return new[]
            {
                new KeyValuePair<int, string>(1, "A"),
                new KeyValuePair<int, string>(2, "B"),
                new KeyValuePair<int, string>(3, "C"),
                new KeyValuePair<int, string>(4, "D"),
            };
        }
    }
}