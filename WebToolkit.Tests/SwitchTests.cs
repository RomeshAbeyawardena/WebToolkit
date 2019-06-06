using System;
using System.Collections.Generic;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class SwitchTests
    {
        [Fact]
        public void Create_with_DictionaryBuilder_invoke_as_parameter()
        {
            var sut = Switch<string, int>.Create(builder => builder
                .Add("moo", 1)
                .Add("baa", 2)
                .Add("woof", 3)
                .Add("raa", 4));

            
            Assert.Contains("moo", sut.ToDictionary());
            Assert.Contains("baa", sut.ToDictionary());
            
            Assert.Contains("woof", sut.ToDictionary());
            Assert.Contains("raa", sut.ToDictionary());
        }

        [Fact]
        public void Create_with_dictionary_parameter_includes_existing_entries()
        {
            var sut = Switch<string, int>
                .Create(new Dictionary<string, int>(
                    new[]
                    {
                        new KeyValuePair<string, int>("some", 1),
                        new KeyValuePair<string, int>("fun", 2), 
                    }));

            Assert.IsType<Switch<string, int>>(sut);

            Assert.Contains("some", sut.ToDictionary());
            Assert.Contains("fun", sut.ToDictionary());
        }

        [Fact]
        public void Create_with_empty_parameter_generates_empty_list()
        {
            var sut = Switch<string, int>.Create();
            Assert.Empty(sut);
        }

        [Fact]
        public void CaseWhen_throws_ArgumentException_when_key_exists()
        {
            Assert.Throws<ArgumentException>(() => { Switch<string, int>.Create()
                .CaseWhen("moo", 1)
                .CaseWhen("moo", 1); });
        }

        [Fact]
        public void CaseWhen_throws_ArgumentNullException_when_delegate_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => { 
                var sut = Switch<string, int>.Create()
                    .CaseWhen("moo", default(Func<int>)); });
        }

        [Fact]
        public void CaseWhen_adds_entry_to_internal_dictionary_when_key_is_unique()
        {
            var sut = Switch<string, int>.Create()
                .CaseWhen("moo", 1)
                .CaseWhen("boo", () => 1 + 1);
            Assert.Contains("moo", sut.ToDictionary());
            Assert.Contains("boo", sut.ToDictionary());
        }

        [Fact]
        public void Case_returns_value()
        {
            var sut = Switch<string, int>.Create()
                .CaseWhen("moo", 1)
                .CaseWhen("boo", () => 1 + 1);

            Assert.Equal(1, sut.Case("moo"));
            Assert.Equal(2, sut.Case("boo"));
        }


        [Fact]
        public void Case_throws_ArgumentException_when_key_not_exists()
        {
            var sut = Switch<string, int>.Create()
                .CaseWhen("moo", 1)
                .CaseWhen("boo", () => 1 + 1);

            Assert.Throws<ArgumentException>(()=> sut.Case("mut"));
        }
    }
}