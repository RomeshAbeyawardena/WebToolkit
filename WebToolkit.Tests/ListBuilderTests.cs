using System.Collections.Generic;
using Moq;
using WebToolkit.Common.Builders;
using Xunit;

namespace WebToolkit.Tests
{
    public class ListBuilderTests
    {
        [Fact]
        public void Create_with_empty_parameter_internal_List_is_empty()
        {
            var sut = ListBuilder<string>.Create();
            Assert.Empty(sut);
        }

        [Fact]
        public void Create_with_item_parameters_internal_list_contains_items()
        {
            var sut = ListBuilder<string>.Create(new []{ "meow", "bah" });
            Assert.Contains("meow", sut);
            Assert.Contains("bah", sut);
        }

        
        [Fact]
        public void Indexer_returns()
        {
            
            _listMock = SetupMockList();
            _listMock.Setup(a => a[It.IsAny<int>()]).Verifiable();
            var sut = ListBuilder<string>.Create(_listMock.Object);
            var result = sut[1];
            _listMock.Verify(a => a[It.IsAny<int>()], Times.Once);
        }

        [Fact]
        public void Add_calls_internal_List_Add()
        {
            _listMock = SetupMockList();
            _listMock.Setup(a => a.Add(It.IsAny<string>())).Verifiable();
            var sut = ListBuilder<string>.Create(_listMock.Object);
            sut.Add("Strut");
            _listMock.Verify(a => a.Add(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void AddRange_calls_internal_List_Add()
        {
            _listMock = SetupMockList();
            _listMock.Setup(a => a.Add(It.IsAny<string>())).Verifiable();
            var sut = ListBuilder<string>.Create(_listMock.Object);
            sut.AddRange(new []{ "Strut", "Meow", "Woof"});
            _listMock.Verify(a => a.Add(It.IsAny<string>()), Times.Exactly(3));
        }

        
        [Fact]
        public void Contains_calls_internal_List_Contains()
        {
            _listMock = SetupMockList();
            _listMock.Setup(a => a.Contains(It.IsAny<string>())).Verifiable();
            var sut = ListBuilder<string>.Create(_listMock.Object);
            sut.Contains("Strut");
            _listMock.Verify(a => a.Contains(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ToArray_returns()
        {
            var sut = ListBuilder<string>.Create(new []{ "meow", "bah" });
            Assert.Contains("meow", sut.ToArray());
        }

        [Fact]
        public void ToList_returns()
        {
            
            var sut = ListBuilder<string>.Create(new []{ "meow", "bah" });
            Assert.Contains("meow", sut.ToList());
        }

        private Mock<IList<string>> SetupMockList()
        {
            return new Mock<IList<string>>();
        }

        private Mock<IList<string>> _listMock;

    }
}